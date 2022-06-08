import { initializeApp } from "firebase/app";
import { getAuth} from "firebase/auth";

const firebaseConfig = {
  apiKey: "AIzaSyDvVH9_vmAYYnRrwNS1LKxwCyVKCE0lmyQ",
  authDomain: "fogasinaplo-4410d.firebaseapp.com",
  projectId: "fogasinaplo-4410d",
  storageBucket: "fogasinaplo-4410d.appspot.com",
  messagingSenderId: "537425067162",
  appId: "1:537425067162:web:3008eeb37cccf2f3d4b323",
};

const app = initializeApp(firebaseConfig)
const auth = getAuth(app)

export { auth }