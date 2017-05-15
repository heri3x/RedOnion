using System;
using UnityEngine;

/// <summary>
/// シングルトンクラス作成用ユーティリティクラス
/// </summary>
/// <typeparam name="T">対象クラス名</typeparam>
/// <example>
/// <code>
/// [Singleton("", true)]
/// public class GameManager : MonoBehaviour
/// {
///     public static GameManager Instance
///     {
///         get { return SingletonUtility&lt;GameManager&gt;.Instance; }
///     }
///     private void Awake()
///     {
///         SingletonUtility&lt;GameManager&gt;.HanldeAwake(this);
///     }
///     private void OnDestroy()
///     {
///         SingletonUtility&lt;GameManager&gt;.HanldeOnDestroy(this);
///     }
/// }
/// </code>
/// </example>
public class SingletonUtility<T> where T : MonoBehaviour
{
    /// <summary>
    /// もしシングルトンインスタンスがなければ生成する
    /// </summary>
    public static void EnsureInstance()
    {
        T instance = Instance;
    }

    /// <summary>
    /// MonoBehaviour.Awake()を処理する
    /// </summary>
    public static void HanldeAwake(T target)
    {
        Debug.Log("HanldeAwake() - シングルトンクラス " + typeof(T) + " のインスタンス初期化");
        if (UnityEngine.Object.FindObjectsOfType<T>().Length > 1)
        {
            UnityEngine.Object.DestroyImmediate(target);
        }
    }

    /// <summary>
    /// MonoBehaviour.OnDestroy()を処理する
    /// </summary>
    public static void HanldeOnDestroy(T target)
    {
        Debug.Log("HanldeOnDestroy() - シングルトンクラス " + typeof(T) + " のインスタンス破棄");
        m_instantiated = false;
    }

    /// <summary>
    /// インスタンスへのアクセスを提供する
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_instantiated)
            {
                return m_instance;
            }

            var type = typeof(T);
            var objects = UnityEngine.Object.FindObjectsOfType<T>();

            if (objects.Length > 0)
            {
                m_instance = objects[0];
                if (objects.Length > 1)
                {
                    Debug.LogWarning("シングルトンクラス " + type + "のインスタンスが複数存在するため自動削除");
                    for (var i = 1; i < objects.Length; i++)
                    {
                        UnityEngine.Object.DestroyImmediate(objects[i].gameObject);
                    }
                }
                m_instantiated = (m_instance != null);
                return m_instance;
            }

            Debug.Log("シングルトンクラス " + type + "のインスタンス生成");
            var attribute = Attribute.GetCustomAttribute(type, typeof(SingletonAttribute)) as SingletonAttribute;
            if (attribute == null)
            {
                Debug.LogError(type + "型にSingletonAttributeが付加されていない");
                Debug.Assert(false);
                return null;
            }

            var prefabPath = attribute.PrefabPath;
            if (String.IsNullOrEmpty(prefabPath))
            {
                // 新規作成
                var createObject = new GameObject(type.ToString(), type);
                m_instance = createObject.GetComponent<T>();
            }
            else
            {
                // Resourcesフォルダ内のプレファブから作成
                var gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(prefabPath)) as GameObject;
                if (gameObject == null)
                {
                    Debug.LogError("\"type\"" + "型のPrefab\"" + prefabPath + "\"を生成できませんでした");
                    return null;
                }
                gameObject.name = prefabPath;
                m_instance = gameObject.GetComponent<T>();
                if (m_instance == null)
                {
                    Debug.LogWarning("\"" + type + "\"型のComponentが\"" + prefabPath + "\"に存在しなかったため追加されました");
                    m_instance = gameObject.AddComponent<T>();
                }
            }

            m_instantiated = (m_instance != null);
            if (m_instantiated && attribute.Persistent)
            {
                UnityEngine.Object.DontDestroyOnLoad(m_instance.gameObject);
            }
            return m_instance;
        }
    }


    private static T m_instance;
    private static bool m_instantiated;
}

/// <summary>
/// シングルトンクラスの属性
/// </summary>
/// <remarks><seealso cref="SingletonUtility{T}"/>も参照。</remarks>
[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class SingletonAttribute : Attribute
{
    /// <summary>
    /// Resourcesフォルダ以降のPrefabのパス
    /// </summary>
    public readonly string PrefabPath;

    /// <summary>
    /// シーンロード時に消失しないオブジェクトであるべきかどうか
    /// </summary>
    public readonly bool Persistent;

    /// <summary>
    /// シングルトンクラス属性を生成する
    /// </summary>
    /// <param name="persistent">シーン変更時に消失しないオブジェクトにするかどうか</param>
    public SingletonAttribute(string prefabPath, bool persistent)
    {
        PrefabPath = prefabPath;
        Persistent = persistent;
    }
}
