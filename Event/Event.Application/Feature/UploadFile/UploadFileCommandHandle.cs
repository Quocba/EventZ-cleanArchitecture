//using System;
//using System.IO;
//using System.Threading;
//using System.Threading.Tasks;
//using MediatR;
//using Microsoft.AspNetCore.Http;

//namespace Event.Application.Feature.UploadFile
//{
//    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand>
//    {
//        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

//        public async Task Handle(UploadFileCommand request, CancellationToken cancellationToken)
//        {
//            if (request.Files == null || request.Files.Count == 0)
//            {
//                throw new ArgumentException("No files received for upload.");
//            }

//            if (!Directory.Exists(_uploadPath))
//            {
//                Directory.CreateDirectory(_uploadPath);
//            }

//            foreach (var file in request.Files)
//            {
//                if (file.Length > 0)
//                {
//                    var filePath = Path.Combine(_uploadPath, file.FileName);

//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await file.CopyToAsync(stream, cancellationToken);
//                    }
//                }
//            }
//        }
//    }
//}
