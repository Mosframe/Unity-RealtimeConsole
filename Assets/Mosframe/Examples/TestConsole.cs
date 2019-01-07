/*
 * TestConsole.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */
namespace Mosframe {

    using System.Text;
    using System.Collections;
    using UnityEngine;

    public class TestConsole : MonoBehaviour {
        private void Start () {

            StartCoroutine( this.onTest() );
        }

        public void onClickOpenConsole () {

            if( RealtimeConsole.Instance.isOpen )
                RealtimeConsole.Instance.close();
            else
                RealtimeConsole.Instance.open();
        }

        IEnumerator onTest () {

            yield return new WaitForSeconds( 1.0f );

            while( true ) {


                var sb = new StringBuilder();


                sb.Append( RichText.bold( RichText.color( "Test : 한글테스트 : 1234567890\nABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz", HtmlColor.convert( new Color32( (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), 255 ) ) ) ) );
                sb.Append( RichText.color( Random.Range( 1000, 10000 ), HtmlColor.convert( new Color32( (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), 255 ) ) ) ).Append( '\n' );

                sb.Append( RichText.color( "Test Value : ", HtmlColor.convert( new Color32( (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), 255 ) ) ) );
                sb.Append( RichText.color( Random.Range( 100000, 1000000 ), HtmlColor.convert( new Color32( (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), (byte)Random.Range( 0, 256 ), 255 ) ) ) ).Append( '\n' );

                var p = Random.Range(0,100);
                if( p < 30 ) {
                    Debug.LogError( sb );
                }
                else
                if( p < 60 ) {
                    Debug.LogWarning( sb );
                }
                else {
                    Debug.Log( sb );
                }



                yield return new WaitForSeconds( 0.5f );
            }
        }

    }
}