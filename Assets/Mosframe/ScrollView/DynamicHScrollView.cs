/**
 * DynamicHScrollView.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

 namespace Mosframe {

    using UnityEngine;

    /// <summary>
    /// Dynamic Horizontal Scroll View
    /// </summary>
    [AddComponentMenu("UI/Dynamic H Scroll View")]
    public class DynamicHScrollView : DynamicScrollView {

        protected override float contentAnchoredPosition    { get { return this._contentRect.anchoredPosition.x; } set { this._contentRect.anchoredPosition = new Vector2( value, this._contentRect.anchoredPosition.y ); } }
	    protected override float contentSize                { get { return this._contentRect.rect.width; } }
	    protected override float viewportSize               { get { return this._viewportRect.rect.width; } }

        protected override void Awake() {

            base.Awake();
            this._direction = Direction.Horizontal;
            this._itemSize = this.itemPrototype.rect.width;
        }
        public override void init () {

            this._direction = Direction.Horizontal;
            base.init();
        }
    }
}
