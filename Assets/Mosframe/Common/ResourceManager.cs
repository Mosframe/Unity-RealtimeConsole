/**
 * ResourceManager.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe
{
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// Resource Manager : Resources폴더의 리소스들을 관리한다.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public class ResourceManager : MonoBehaviour
    {
        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static ResourceManager I
        {
            get
            {
                if( _instance == null )
                {
                    var go = new GameObject("ResourceManager", typeof(ResourceManager));
                    _instance = go.GetComponent<ResourceManager>();
                    if( Application.isPlaying )
                    {
                        DontDestroyOnLoad( _instance.gameObject );
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="poolName">풀 이름</param>
        /// <param name="initialSize">초기 수량</param>
        /// <param name="increaseSize">풀이 팽창될때마다 증가될 수량</param>
        public void initialize( string poolName, int initialSize=1, int increaseSize=1 )
        {
            if( this._pool.ContainsKey(poolName) ) return;

            var poolObject = Resources.Load<GameObject>(poolName);
            if( poolObject == null )
            {
                Debug.LogError( RichText.red( "Resources.Load : " + new{poolName} ) );
                return;
            }
            this._pool[poolName] = new GameObjectPool( poolName, poolObject, this.transform, initialSize, increaseSize );
        }

        /// <summary>
        /// 오브젝트 꺼내기
        /// </summary>
        /// <param name="poolName">풀이름</param>
        /// <returns></returns>
        public GameObject getObject( string poolName )
        {
            if( !this._pool.ContainsKey(poolName) )
            {
                initialize( poolName, 0, 1 );
            }
            return this._pool[poolName].getObject();
        }

        /// <summary>
        /// 오브젝트 반환
        /// </summary>
        /// <param name="go">GameObject</param>
        public void releaseObject( GameObject go )
        {
            var poolObject = go.GetComponent<GameObjectPoolItem>();
            if( poolObject == null )
            {
                Debug.LogError( RichText.red( new{poolObject} ) );
                return;
            }
            this._pool[poolObject.poolName].releaseObject(poolObject);
        }

        #region [ Private Variables ]
        
        private         Dictionary<string, GameObjectPool> _pool       = new Dictionary<string, GameObjectPool>();
        private static  ResourceManager                     _instance   = null;

        #endregion [ Private Variables ]
    }
}