namespace LibraryApi.Applications.Exceptions;
/// <summary>
/// 要求されたリソースが見つからない場合にスローされる例外
///
/// アプリケーション層が、リポジトリを通じた検索の結果として対象が存在しないことを、
/// プレゼンテーション層へ通知するために用いる(HTTP 404 Not Found に対応づける想定)。
///
/// データレベルの不在を表すインフラ層の例外(EntityNotFoundException)とは役割が異なり、
/// 本例外はユースケースの結果としての「リソース不在」を表す。
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public NotFoundException() : base() { }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="message">エラーメッセージ</param>
    public NotFoundException(string message) : base(message) { }
}