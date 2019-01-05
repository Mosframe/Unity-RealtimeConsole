/**
 * DynamicVScrollView.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

 namespace Mosframe {

    using UnityEngine;

    /// <summary>
    /// Dynamic Vertical Scroll View
    /// </summary>
    [AddComponentMenu("UI/Dynamic V Scroll View")]
    public class DynamicVScrollView : DynamicScrollView {

        protected override float contentAnchoredPosition    { get { return -this._contentRect.anchoredPosition.y; } set { this._contentRect.anchoredPosition = new Vector2( this._contentRect.anchoredPosition.x, -value ); } }
	    protected override float contentSize                { get { return this._contentRect.rect.height; } }
	    protected override float viewportSize               { get { return this._viewportRect.rect.height;} }

        protected override void Awake() {

            base.Awake();
            this._direction = Direction.Vertical;
            this._itemSize = this.itemPrototype.rect.height;
        }
        public override void init () {

            this._direction = Direction.Vertical;
            base.init();
        }
    }
}
