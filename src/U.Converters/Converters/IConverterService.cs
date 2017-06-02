using System.Drawing;
using System.Drawing.Imaging;
using U.Application.Services;

namespace U.Converters
{
    /// <summary>
    /// 应用转换服务
    /// 如：word转pdf、html转image、html转word 等等
    /// </summary>
    public interface IConverterService : IApplicationService
    {
        /// <summary>
        /// Html 转 image 对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="savePath">是否保存到路径</param>
        /// <param name="format">保存图片时使用</param>
        /// <returns></returns>
        Image HtmlToImage(string url, int width, int height, string savePath = "", ImageFormat format = null);

        /// <summary>
        /// Html 转 image 对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="savePath">是否保存到路径</param>
        /// <param name="format">保存图片时使用</param>
        /// <returns></returns>
        byte[] HtmlToImageArray(string url, int width, int height, string savePath = "", ImageFormat format = null);
    }
}
