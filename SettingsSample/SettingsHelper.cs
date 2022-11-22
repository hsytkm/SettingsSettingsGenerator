using System;
using SettingsSettingsGenerator;

namespace SettingsSample;

/// <summary>
/// Settings.settings の値を管理するクラスです。
/// </summary>
public sealed partial class SettingsHelper : IDisposable
{
    [SettingsGenerator(nameof(Settings1.Default.MyInt))]
    private int? _myInt;

    [SettingsGenerator(nameof(Settings1.Default.MyString))]
    private string? _myString;

    [SettingsGenerator(nameof(Settings1.Default.MyBool))]
    bool? _myBool;

    public void Dispose() => Save();
}
