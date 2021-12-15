namespace Bing.Wallpaper.Entities;

public class ImageMetadata
{
    /// <summary>
    /// 제목
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 가로 픽셀
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// 세로 픽셀
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// 원본출처
    /// </summary>
    public string Origin { get; set; }

    /// <summary>
    /// 저작권
    /// </summary>
    public string Copyright { get; set; }

    /// <summary>
    /// 저작권 링크
    /// </summary>
    public string CopyrightLink { get; set; }
}
