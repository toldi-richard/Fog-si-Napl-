<template>
	<div class="background">
		<div class="forms">
			<form class="signup" @submit.prevent="register">
				<h1>Regisztráció</h1>
				<div id="signup"> 
					<i class="fa fa-user"/>		
					<input 
					type="email" 
					placeholder="Email cím"
					v-model="register_form.email"
					@keydown.tab="getData()"/>
				</div>
				<div id="signup"> 
					<i class="fa fa-key"/>	
					<input 
						type="password" 
						placeholder="Jelszó" 
						v-model="register_form.password"
						@click="getData()"/>
					</div>
				<input 
					type="submit" 
					value="Regisztráció"
					:disabled="valid" />
                <p id="forgetPass" @click="forgot">Jelszó emlékeztető</p>
				<p><router-link to="/login">Bejelentkezés</router-link></p>
			</form>
		</div>
	</div>
</template>

<script>
import { ref } from 'vue'
import { useStore } from 'vuex'
import axios from 'axios';

export default {
	data() {
		return {
			response: null,
			valid: true
		}
	},
	setup () {
		const register_form = ref({});
		const store = useStore();
		const register = () => {
			store.dispatch('register', register_form.value);
			localStorage.setItem("email", register_form.value.email)
		axios.get('http://localhost:5000/api/Felhasznalok/Azonosito/'+register_form.value.email)
        .then(response => {
        localStorage.setItem("id", response.data)
        })
        .catch(error => console.log(error))

        axios.get('http://localhost:5000/api/Felhasznalok/Szerepkor/'+register_form.value.email)
          .then(response => {
          localStorage.setItem("role", response.data)
          })
          .catch(error => console.log(error))
		}
		return {
			register_form,
			register
		}
	},
	created() {
		this.$swal.fire({
			icon: 'info',
			title: 'Tájékoztató',
			text: "Csak horgász engedéllyel rendelkezők tudnak regisztrálni! "+
			"Ha még nincs engedélye keresse fel az önhöz legközelebbi Horgász Egyesületet! "})
	},
	methods: {
		getData(){
			axios.get('http://localhost:5000/api/Felhasznalok/Azonosito/'+this.register_form.email)
			.catch(error => {console.log(error);this.response = null; this.error()})
			.then(response => {this.response = response.data; this.error()})
			if (this.register_form.password != null) {
				this.valid = false
			}
		},
		error(){
			if (this.response!=null) {
				this.$swal.fire({
					icon: 'success',
					title: 'Regisztrálhat!',
					text: 'Üdvözöljük, '+ this.response + ' !'});
					this.valid = false
			} else {
				this.$swal.fire({
					icon: 'error',
					title: 'Hiba!',
					text: "Ezzel az email címmel "+this.register_form.email+" nem regisztrálhat! Keresse fel a Horgász Egyesületét a további információk érdekében!"})
					this.valid = true;
			}
		},
	forgot() {
        this.$swal.fire({
        icon: 'info',
        title: 'Fejlesztés',
        text: 'A funkció fejlesztés alatt áll!',
        confirmButtonText: 'Ok'})},
	},
}
</script>


<style scoped>

.background {
    transition: 1000ms;
    background-image: url('~@/assets/basic.jpg');
    background-size: cover;
    min-height: 100vh;
    padding-top: 50px;
    }

i {
    font-size: 2rem;
    padding-right: 20px;
    padding-top: 15px;
    color: #102770;
}
#signup{
	display: flex;
	flex-direction: row 
}

.forms {
    background-color: white;
    display: flex;
    min-height: 70vh;
    max-width: 500px;
    margin: auto;
    box-shadow: 10px 10px 15px  rgba(7, 7, 7, 0.5);
    border-radius: 40px;
}

form {
	flex: 1 1 0%;
	padding: 8rem 1rem 1rem;
}

h1 {
    font-family: 'Zen Antique', serif;
    color: #102770;
    font-size: 2.5rem;
    text-transform: uppercase;
    margin-bottom: 4rem;
    white-space:normal;
    word-break: break-all;
    text-align:center;
}

input {
    appearance: none;
    border: none;
    outline: none;
    background: none;
    display: block;
    width: 100%;
    max-width: 400px;
    margin: auto auto 2rem auto;
    font-size: 1.5rem;
    padding: 0.5rem 0rem;
}

input::placeholder {
    font-family: 'Zen Antique', serif;
    color: #102770;
}

form.signup input:not([type="submit"]) {
    color: #2c3e50;
    border-bottom: 2px solid #2c3e50;
}

form.signup input[type="submit"] {
    opacity: 95% !important;
    background: #13335c;
    font-family:'Montserrat', sans-serif !important;
    font-weight: bold;
    font-size: 1.5rem;
    display: flex;
    margin: auto;
    margin-top: 40px;
    text-align: center;
    width: 230px;
    height: 60px;
    box-shadow: 1px 2px 10px rgb(51, 51, 51);
    border-radius: 35px;
    color: whitesmoke;
    cursor: pointer;
    display: block;
}
form.signup input[type="submit"]:hover{
    color:azure;
    background: linear-gradient(90deg, #9ebd13 0%, #008552 100%);
    box-shadow: -10px -10px 15px rgba(192, 192, 192, 0.12),
                10px 10px 15px  rgba(0, 0, 0, 0.5);
}

p {
    font-weight: bold;
}
#forgetPass {
    color: black;
    margin-top: 150px;
    font-weight: bold;
}
#forgetPass:hover{
	text-decoration: none;
    color: #2171cf; 
}

@media only screen and (min-height: 1000px) {
#forgetPass {
		margin-top: 150px;
	}
}

a { 
	text-decoration: none;
	color: black; 
 }

 a:hover { 
    text-decoration: none;
    color: #2171cf; 
 }
</style>