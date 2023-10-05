using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// << 싱글톤 패턴 사용 설명 >>
// 싱글톤 패턴을 적용하고자 하는 클래스에 Singleton<T> 클래스를 상속받도록 설정합니다.
// 사용 예시:
// public class ExampleClass : Singleton<ExampleClass>
// {
//     // 클래스의 멤버 변수 및 메서드를 정의할 수 있습니다.
// }

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object lockObject = new object();
    private static T instance = null;
    private static bool IsQuitting = false;

    // 싱글톤 인스턴스 반환 프로퍼티
    public static T Instance
    {
        get
        {
            lock (lockObject)
            {
                if (IsQuitting) // 어플리케이션 종료 중인 경우 null 반환
                {
                    return null;
                }
                if (instance == null) // 인스턴스가 생성되어 있지 않은 경우 새로운 객체를 생성해서 만들어 준 후 반환
                {
                    // Scene에서 서치
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null) // 현재 Scene에 없을 경우 새로운 GameObject 생성 후 컴포넌트 부착
                    {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).Name;
                    }
                }
                return instance;
            }
        }
    }
    private void OnDisable()
    {

        IsQuitting = true;
        instance = null;
    }
}
