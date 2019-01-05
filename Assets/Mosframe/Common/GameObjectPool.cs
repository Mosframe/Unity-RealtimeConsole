/**
 * GameObjectPool.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe
{
    using UnityEngine;
    using System.Collections.Generic;


    /// <summary>
    /// 플 오브젝트 속성 컴포넌트
    /// </summary>
    //[DisallowMultipleComponent]
    public class GameObjectPoolItem : MonoBehaviour
    {
        public string   poolName;
        public bool     isPooled;
    }

    /// <summary>
    /// GameObject Poll
    /// </summary>
    public class GameObjectPool
    {
        /// <summary>
        /// 풀 생성
        /// </summary>
        /// <param name="poolName">풀 이름</param>
        /// <param name="poolObject">풀오브젝트 프리팹</param>
        /// <param name="root">풀을 추가할 부모 트랜스폼</param>
        /// <param name="initialSize">초기 생성될 수량</param>
        /// <param name="increaseSize">풀이 팽창될때마다 증가될 수량</param>
        public GameObjectPool( string poolName, GameObject poolObject, Transform root, int initialSize=1, int increaseSize=1 )
        {
            this._poolName          = poolName;
            this._increaseSize      = increaseSize;

            // 루트 오브젝트 생성
            this._rootObj = new GameObject(poolName + "Pool");
            this._rootObj.transform.SetParent(root, false);

            // 프로토타입 인스턴스 생성
            var go = Object.Instantiate(poolObject);
            var po = go.AddComponent<GameObjectPoolItem>();
            po.poolName = poolName;
            this._addObject(po);

            // 초기 객체를 추가한다. ( 1개 이상 )
            this._populate( Mathf.Max(initialSize,1) );
        }
        
        /// <summary>
        /// 풀에서 오브젝트 꺼내기
        /// </summary>
        public GameObject getObject()
        {
            GameObjectPoolItem po = null;
            if( this._stack.Count > 1 )
            {
                po = this._stack.Pop();
            }
            else
            {
                #if UNITY_EDITOR
                Debug.Log( new { this._poolName, this._increaseSize } );
                #endif

                this._populate( this._increaseSize );
                po = this._stack.Pop();
            }

            po.isPooled = false;
            return po.gameObject;
        }

        /// <summary>
        /// 사용된 오브젝트 반환
        /// </summary>
        public void releaseObject( GameObjectPoolItem po )
        {
            if( this._poolName.Equals(po.poolName) )
            {
                if( po.isPooled )
                {
                    Debug.LogWarning( new { po.gameObject.name, po.isPooled });
                }
                else
                {
                    this._addObject(po);
                }
            }
        }


        #region [ Private Variables ]

        private Stack<GameObjectPoolItem>  _stack = new Stack<GameObjectPoolItem>(); // 풀 오브젝트 스택
        private GameObject          _rootObj; // 루트 오브젝트
        private int                 _increaseSize; // 팽창될때마다 증가될 수량
        private string              _poolName;

        #endregion [ Private Variables ]

        #region [ Private Functions ]

        // 풀에 오브젝트들 채우기
        private void _populate( int count )
        {
            for( var c=0; c<count; ++c )
            {
                this._addObject( Object.Instantiate( this._stack.Peek() ) );
            }
        }

        // 풀에 풀 오브젝트 추가
        private void _addObject( GameObjectPoolItem po )
        {
            po.gameObject.SetActive(false);
            po.gameObject.name = this._poolName;
            this._stack.Push(po);
            po.isPooled = true;

            po.gameObject.transform.SetParent(this._rootObj.transform, false);
        }

        #endregion [ Private Functions ]
    }
}