public static class Extension_C
{
    private const string format0N0 = "{0:N0}"; // 소수점 0자리 형식
    private const string format0N1 = "{0:N1}"; // 소수점 1자리 형식
    private const string format0N2 = "{0:N2}"; // 소수점 2자리 형식
    private const string format0N3 = "{0:N3}"; // 소수점 3자리 형식

    // float 값을 문자열로 변환하는 확장 메서드
    public static string ToString_Func(this float _value, int _pointNumber = 0)
    {
        if (_pointNumber > 0)
        {
            if (_pointNumber == 1)
                return string.Format(format0N1, _value); // 소수점 1자리 형식으로 변환
            else if (_pointNumber == 2)
                return string.Format(format0N2, _value); // 소수점 2자리 형식으로 변환
            else if (_pointNumber == 3)
                return string.Format(format0N3, _value); // 소수점 3자리 형식으로 변환
            else
                return _value.ToString(); // 기본 형식으로 변환
        }
        else
        {
            return string.Format(format0N0, _value); // 소수점 0자리 형식으로 변환
        }
    }

    // float 값을 퍼센트 문자열로 변환하는 확장 메서드
    public static string ToString_Percent_Func(this float _value)
    {
        return (_value * 100f).ToString_Func() + "%"; // 퍼센트 형식으로 변환
    }
}


