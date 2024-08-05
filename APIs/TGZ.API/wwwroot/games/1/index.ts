import * as THREE from 'three'
import { OrbitControls} from 'three/examples/jsm/controls/OrbitControls'

export const Play = () => {
    const canvas = document.getElementsByTagName('canvas')[0] as HTMLCanvasElement

    const scene = new THREE.Scene()
    const camera = new THREE.PerspectiveCamera(75, canvas.clientWidth / canvas.clientHeight, 0.1, 1000);
    const renderer = new THREE.WebGLRenderer({
        canvas: canvas
    });
    renderer.setSize(canvas.clientWidth, canvas.clientHeight)
    
    const geometry = new THREE.BoxGeometry( 1, 1, 1 );
    const material = new THREE.MeshBasicMaterial( { color: 0x00ff00 } );
    const cube = new THREE.Mesh( geometry, material );
    scene.add( cube );
    
    camera.position.z = 5;
    
    const controls = new OrbitControls(camera, renderer.domElement)
    controls.update()
    
    function animate() {
        controls.update()
        renderer.render( scene, camera );
    }
    
    renderer.setAnimationLoop( animate );
}