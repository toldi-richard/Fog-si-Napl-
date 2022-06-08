import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'jquery/src/jquery.js'
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';
import "datatables.net-bs4";
import "datatables.net-bs4/css/dataTables.bootstrap4.min.css";


const options = {
    confirmButtonColor: '#2272d2'
  };

createApp(App).use(store).use(router).use(VueSweetalert2, options).mount('#app')
