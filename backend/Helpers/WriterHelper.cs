using System.Text;
using System.Text.RegularExpressions;

namespace backend.Helpers;

public class WriterHelper
{
    public enum ImageFormat
    {
        bmp,
        jpeg,
        tiff,
        png,
        unknown
    }

    // For more file signatures, see the File Signatures Database (https://www.filesignatures.net/)
    // and the official specifications for the file types you wish to add.
    private static readonly Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
        {
            { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                    new byte[] { 0x49, 0x46, 0x00, 0x01 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                }
            },
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                    new byte[] { 0x49, 0x46, 0x00, 0x01 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                }
            },
            { ".zip", new List<byte[]>
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                    new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45 },
                    new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                    new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                    new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                    new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 },
                }
            },
            { ".pdf", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } },
            { ".xls", new List<byte[]>
                {
                    new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                    new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x10 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x1F },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x22 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x23 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x28 },
                    new byte[] { 0xFD, 0xFF, 0xFF, 0xFF, 0x29 }
                }
            },
            { ".xlsx", new List<byte[]>
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                    new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }
                }
            },
            { ".mp4", new List<byte[]>
                {
                    new byte[] { 0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56 },
                    new byte[] { 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D }
                }
            },
        };

    public static ImageFormat GetImageFormat(byte[] bytes)
    {
        var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
        var png = new byte[] { 137, 80, 78, 71 };    // PNG
        var tiff = new byte[] { 73, 73, 42 };         // TIFF
        var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
        var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
        var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon
        var jpeg3 = new byte[] { 255, 216, 255, 219 };
        var jpeg4 = new byte[] { 73, 70, 0, 1 };
        var jpeg5 = new byte[] { 255, 216, 255, 238 };
        var jpeg6 = new byte[] { 105, 102, 0, 0 };
        var jpeg7 = new byte[] { 255, 216, 255, 226 };

        if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
            return ImageFormat.bmp;

        if (png.SequenceEqual(bytes.Take(png.Length)))
            return ImageFormat.png;

        if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
            return ImageFormat.tiff;

        if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
            return ImageFormat.tiff;

        if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
            return ImageFormat.jpeg;

        if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
            return ImageFormat.jpeg;

        if (jpeg3.SequenceEqual(bytes.Take(jpeg3.Length)))
            return ImageFormat.jpeg;

        if (jpeg4.SequenceEqual(bytes.Take(jpeg4.Length)))
            return ImageFormat.jpeg;

        if (jpeg5.SequenceEqual(bytes.Take(jpeg5.Length)))
            return ImageFormat.jpeg;

        if (jpeg6.SequenceEqual(bytes.Take(jpeg6.Length)))
            return ImageFormat.jpeg;

        if (jpeg7.SequenceEqual(bytes.Take(jpeg7.Length)))
            return ImageFormat.jpeg;

        return ImageFormat.unknown;
    }

    /// <summary>
    /// Strip illegal chars and reserved words from a candidate filename (should not include the directory path)
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/309485/c-sharp-sanitize-file-name
    /// </remarks>
    public static string CoerceValidFileName(string filename)
    {
        string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
        string invalidReStr = string.Format(@"[ {0}]+", invalidChars);

        string[] reservedWords = new[]
        {
                "CON", "PRN", "AUX", "CLOCK$", "NUL", "COM0", "COM1", "COM2", "COM3", "COM4",
                "COM5", "COM6", "COM7", "COM8", "COM9", "LPT0", "LPT1", "LPT2", "LPT3", "LPT4",
                "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
            };

        string sanitisedNamePart = Regex.Replace(filename, invalidReStr, "_");
        foreach (var reservedWord in reservedWords)
        {
            var reservedWordPattern = string.Format("^{0}\\.", reservedWord);
            sanitisedNamePart = Regex.Replace(sanitisedNamePart, reservedWordPattern, "_reservedWord_.", RegexOptions.IgnoreCase);
        }

        return sanitisedNamePart;
    }

    /// <summary>
    /// File signature check
    /// </summary>
    /// <remarks>
    /// With the file signatures provided in the _fileSignature
    /// dictionary, the following code tests the input content's
    /// file signature.
    /// </remarks>
    public static bool IsValidFileExtensionAndSignature(string fileName, Stream data)
    {
        data.Position = 0;

        using var reader = new BinaryReader(data);
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        if (!_fileSignature.ContainsKey(ext))
        {
            return true;
        }
        var signatures = _fileSignature[ext];
        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

        return signatures.Any(signature =>
            headerBytes.Take(signature.Length).SequenceEqual(signature));
    }
}