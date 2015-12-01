using System.Collections.Generic;
using System.IO;
using TestModel.Outline;
using TestModel.Tools;
using TestModel.Transformation;
using TestTools.DialogAttribute;
using TestTools.PointAttribute;

namespace TestModel.Source.Ild
{
    [BottomLeft(-128 * 256, -128 * 256)]
    [BottomRight(128 * 256 - 1, -128 * 256)]
    [TopLeft(-128 * 256, 128 * 256 - 1)]
    [TopRight(128 * 256 - 1, 128 * 256 - 1)]
    [FileFormat("ILD")]
    [OpenDialogOptions(Filter = "ILDA Files (*.ild)|*.ild", DefaultExt = "ild")]
    [SaveDialogOptions(Filter = "ILDA Files (*.ild)|*.ild", DefaultExt = "ild")]
    public class IldMultiFrameFactory : CustomFactory, IFrameSaver, IFactory<IEnumerable<IFrameable>>
    {
        public IEnumerable<IFrameable> Create(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (var br = new BinaryReader(fs))
                {
                    var frames = new List<IldFrame>();
                    while (true)
                    {
                        var header = new IldHeader(BytesConverter.ByteToType<IldHeaderRaw>(br));

                        if (header.FormatCode != 0)
                        {
                            return frames;
                        }

                        if (header.TotalPoints == 0 || header.FrameNumber == header.TotalFrames)
                        {
                            return frames;
                        }

                        var records = new List<IldRecord>();
                        while (true)
                        {
                            var record = new IldRecord(BytesConverter.ByteToType<IldRecordRaw>(br));
                            if (record.IsLast)
                            {
                                break;
                            }
                            records.Add(record);
                        }

                        var frame = new IldFrame(header, records, Transformation);
                        frames.Add(frame);
                    }
                }
            }
        }

        public void Save(string fileName, IFrameable frameable)
        {
            var startHeaderRaw = new IldHeaderRaw();
            startHeaderRaw.Init();
            var startHeader = new IldHeader(startHeaderRaw)
            {
                FileFormat = "ILDA",
                FormatCode = 0,
                FrameName = frameable.Name,
                CompanyName = "Peleton",
                TotalPoints = frameable.TracePoints.Length + 1,
                FrameNumber = 0,
                TotalFrames = 1,
                ScannerHead = 0,
                Future = 0
            };

            var finishHeaderRaw = new IldHeaderRaw();
            finishHeaderRaw.Init();
            var finishHeader = new IldHeader(finishHeaderRaw)
            {
                FileFormat = "ILDA",
                FormatCode = 0,
                FrameName = frameable.Name,
                CompanyName = "Peleton",
                TotalPoints = 0,
                FrameNumber = 1,
                TotalFrames = 1,
                ScannerHead = 0,
                Future = 0
            };

            var transformation = new CompositeTransformation(
                new ReverseTransformation(frameable.Transformation),
                Transformation);
            var recordRawArray = new IldRecordRaw[frameable.TracePoints.Length];
            for (var i = 0; i < recordRawArray.Length; i++)
            {
                var tracePoint = frameable.TracePoints[i];
                var recordRaw = new IldRecordRaw();
                recordRaw.Init();

                var point = transformation.Transform(tracePoint.Point);
                
                var record = new IldRecord(recordRaw)
                {
                    X = (int)point.X,
                    Y = (int)point.Y,
                    Z = -16,
                    Status = 56,
                    IsMove = !tracePoint.Trace,
                    IsLast = false
                };
                recordRawArray[i] = recordRaw;
            }

            var finishRecordRaw = new IldRecordRaw();
            finishRecordRaw.Init();

            var finishRecord = new IldRecord(finishRecordRaw)
            {
                X = 0,
                Y = 0,
                Z = -16,
                Status = 56,
                IsMove = true,
                IsLast = true
            };

            SaveToFile(fileName, startHeaderRaw, recordRawArray, finishRecordRaw, finishHeaderRaw);
        }

        private static void SaveToFile(string fileName, 
            IldHeaderRaw startHeaderRaw, 
            IldRecordRaw[] recordRawArray,
            IldRecordRaw finishRecordRaw,
            IldHeaderRaw finishHeaderRaw)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(startHeaderRaw.Data);
                    for (var i = 0; i < recordRawArray.Length; i++)
                    {
                        bw.Write(recordRawArray[i].Data);
                    }
                    bw.Write(finishRecordRaw.Data);
                    bw.Write(finishHeaderRaw.Data);
                    bw.Flush();
                }
            }
        }
    }
}
