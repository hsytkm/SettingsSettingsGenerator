using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SettingsSettingsGenerator;

namespace SettingsSample
{
    /// <summary>
    /// Settings.settings の値を管理するクラスです。
    /// </summary>
    public sealed partial class SettingsHelper : IDisposable
    {
        /* ◆ソースジェネレータを用意したくなるけど我慢
         *    → ソースジェネレータを作りた過ぎて禿げそう。
         */

        [SettingsGenerator(nameof(Settings1.Default.MyInt))]
        private int? _myInt;

        [SettingsGenerator(nameof(Settings1.Default.MyString))]
        private string? _myString;

        [SettingsGenerator(nameof(Settings1.Default.MyBool))]
        private bool? _myBool;

        public void Dispose() => Save();
    }

    //public sealed partial class SettingsHelper : INotifyPropertyChanged
    //{
    //    // 設定値変化の有無を管理しているフラグです
    //    private bool _isChanged;

    //    public int IsConfirmShutdown
    //    {
    //        get
    //        {
    //            _myInt ??= Settings1.Default.MyInt;
    //            return _myInt.Value;
    //        }
    //        set
    //        {
    //            _myInt ??= Settings1.Default.MyInt;
    //            SetProperty(ref _myInt, value);
    //        }
    //    }

    //    /// <summary>
    //    /// 現在の設定を *.settings に書き込みます
    //    /// </summary>
    //    public void Save()
    //    {
    //        if (!_isChanged) return;

    //        // アプリで値を更新した場合に値を書き戻します
    //        if (_myInt.HasValue)
    //            Settings1.Default.MyInt = _myInt.Value;

    //        Settings1.Default.Save();
    //        _isChanged = false;
    //    }

    //    public event PropertyChangedEventHandler? PropertyChanged;

    //    private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    //    {
    //        if (EqualityComparer<T>.Default.Equals(field, value))
    //            return false;

    //        field = value;
    //        _isChanged = true;

    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //        return true;
    //    }
    //}
}
