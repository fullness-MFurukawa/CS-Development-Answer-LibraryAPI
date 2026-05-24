namespace LibraryApi.Applications.Exceptions;
/// <summary>
/// 入力された値が、業務上の妥当性を満たさない場合にスローされる例外
///
/// 形式的な入力検証(必須・文字数など)はプレゼンテーション層が担うのに対し、
/// 本例外は、形式は妥当だが内容が業務的に不正なケース
/// (例:指定された分類が実在しない)を表す。
/// プレゼンテーション層で 400 Bad Request に変換される想定。
///
/// 例外の「意味(入力の不正)」をクラス名で表し、具体的な内容はメッセージで伝える。
/// </summary>
public class InvalidInputException : Exception
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public InvalidInputException() : base() { }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="message">エラーメッセージ(具体的な不正の内容)</param>
    public InvalidInputException(string message) : base(message) { }
}