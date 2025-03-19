using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Infrastructure.Context;

namespace Event.Infrastructure.Queries
{
    public class EventDocumentsQuery(ISqlConnectionFactory _connectionFactory) : IEventDocumentQuery
    {
        public async Task<PagedResponse<List<GetDocumentByEventDTO>>> GetDocumentsByEvent(PagedRequest request,Guid eventID)
        {
            using var connection = _connectionFactory.GetOpenConnection();


            const string sqlQuery = @"
                                    WITH document_ids AS (
                                        SELECT id
                                        FROM event_documents
                                        WHERE event_id = @eventID
                                        ORDER BY id
                                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                                    )
                                    SELECT 
                                        ed.id AS DocumentID,
                                        ed.title AS Title,
                                        ed.content AS Content,
                                        ed.link_document AS LinkDocument,
                                        CAST(ed.documents_type AS VARCHAR) AS DocumentType
                                    FROM document_ids did
                                    INNER JOIN event_documents ed ON did.id = ed.id;";

            var offset = (request.PageNumber - 1) * request.PageSize;

            var result = await connection.QueryAsync<GetDocumentByEventDTO>(
                sqlQuery,
                new { eventID, Offset = offset, PageSize = request.PageSize }
            );

            const string sqlCount = "SELECT COUNT(*) FROM event_documents WHERE event_id = @eventID";
            int totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount, new { eventID });

            return new PagedResponse<List<GetDocumentByEventDTO>>(
                result.ToList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );
        }

    }
}
