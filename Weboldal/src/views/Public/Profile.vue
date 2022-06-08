<template>
	<div class="background" @click="checkButton">
		<div class="container">
			<div class="content">
				<h1>Adatok</h1>
				<div id="data"> 
					<i :id="idStyle" class="fa fa-user"/>
					<p id="user">{{identifierNumber}}</p>
				</div>
				<div id="data"> 
					<i :id="idStyleBadge" class="fa fa-gear"/>
					<p id="user">{{nameOfRole}}</p>
				</div>
				<div id="data">
					<i class="fa fa-envelope"/>
					<p>{{email}}</p>
				</div>
				<button id="first" v-if="user()" class="button2"><router-link router-link to="/MyCatches"><i class="fa fa-fish"></i>Fogásaim</router-link></button>
				
				<button class="button2" data-toggle="modal" data-target="#myModal" @click="editModal"><i id="icon" class="fa fa-edit"></i> Módosítás</button> 

				<button id="last" class="button2" @click="$store.dispatch('logout')"><i id="icon" class="fa fa-door-open"></i> Kijelentkezés</button> 
			</div>

	<!-- Modal -->
     <div class="modal fade" id="myModal">
        <div class="modal-dialog modal-lg modal-dialog-centered">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="ModalLabel">{{ modalHeader}}</h5>
            </div>
            <div class="modal-body">
              <div class="d-flex justify-content-center">
                <div class="p-2 w-50 bd-highlight">

                  <div class="input-group mb-3">
                    <input type="text" class="form-control" v-model="emailM" @keypress="emailValidCheck" required>
                  </div>
                </div>
              </div>
              <nav id="modalButton">
                <button id="save" type="button" class="btn btn-primary" data-dismiss="modal" @click="updateUser" :disabled="emailValid">Mentés</button>
                <button id="close" type="button" class="btn btn-danger" data-dismiss="modal">Bezár</button>
              </nav>
            </div>
          </div>
        </div>
		</div>
        <!-- Modal -->

		</div>
	</div>
</template>

<script>
import axios from 'axios';

export default {
	name: 'Profil',
  data() {
	return{
		nameOfRole: "",
		idStyle: null,
		idStyleBadge: null,
		role: localStorage.getItem("role"),
		email: localStorage.getItem("email"),
		identifierNumber: localStorage.getItem("id"),
		emailM:null,
		roleM:null,
		modalHeader:null,
		regEmail: /[A-z\u00C0-\u00FF0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/,
		emailValid:true,
	}
  },
  created() {
	if(this.role == "1") { this.idStyle = "userIcon",this.idStyleBadge = "userIconBadge", this.nameOfRole="Adminisztrátor"}
	else if(this.role == "2") { this.idStyle = "userIcon2", this.idStyleBadge = "userIconBadge2",this.nameOfRole="Halőr"}
	else if(this.role == "3") {this.idStyleBadge = "userIconBadge3",  this.nameOfRole="Felhasználó"}

	axios.get('http://localhost:5000/api/Felhasznalok/'+localStorage.getItem("id"))
        .then(response => {
        localStorage.setItem("password", response.data.jelszo)
        })
        .catch(error => console.log(error))
  },
  methods: {
	emailValidCheck(){
		if(this.regEmail.test(this.emailM))
		{
			if(this.role === "1" || this.role === "2") 
			{
				this.emailValid = true
				this.alertEmail()
			}
			else {
				this.emailValid = false
				}

		} else {
			this.emailValid = true
		}
	},

	user(){
          if(this.role == 3) {
              return true
          } else {return false}
      },
	editModal(){
        this.modalHeader = "Email módosítása",
		this.emailM = localStorage.getItem("email"),
		console.log(this.licence)
      },

    // Azonosító nem módosítható mert az a primary key
    // ezért érdemes id-t primaryként adni mindennek
    // ha változik az engedélye, akkor azt is lehessen változtatni
    // így csak a törlés és újra felvitel marad...ha azt akarjuk változtatni
    // a belépés a FIREBASE adatbázisával történik, ezért ha módosítja az
    // email-t akkor is még a régivel tud majd belépni egyelőre
    // az adatbázisban módosul, Google Firebase-nél még nem
    updateUser(){
        axios.put('http://localhost:5000/api/Felhasznalok/'+this.identifierNumber, {
            azonosito:this.identifierNumber,
            szerepkorID:this.role,
            jelszo: localStorage.getItem("password"),
            email_cim:this.emailM
      }).then(this.showAlertUpdate(),localStorage.setItem("email", this.emailM));},

	checkButton(){if(this.emailValid==false){this.emailValid=true}else{this.emailValid = false}},
    
    showAlertUpdate() {
        this.$swal.fire({
        icon: 'success',
        title: 'Sikeres módosítás!',
        text: 'A felhasználó adatainak módosítása sikerült! Kérem frissítse az oldalt!',
        confirmButtonText: 'Ok'});
        },

	alertEmail() {
        this.$swal.fire({
        icon: 'error',
        title: 'Letiltva!',
        text: 'A változtatáshoz keresse fel a rendszergazdát!',
        confirmButtonText: 'Ok'});
        },
  }
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
	color: black;
}

#icon{
	padding-top: 5px;
	font-size: 25px;
}

.button2{
	display: flex;
}

a { 
	text-decoration: none;
	color: black;
	padding: 0px 35px 0px 5px;
 }

 a:hover { 
    text-decoration: none;
    color: white;
 }


#data{
	display: flex;
	flex-direction: row;
	margin: 15px auto;
}

.container {
    background-color: white;
    display: flex;
    min-height: 70vh;
	margin-top: 120px!important;
    max-width: 500px;
    margin: auto;
    box-shadow: 10px 10px 15px  rgba(7, 7, 7, 0.5);
    border-radius: 40px;
}

.content {
	flex: 1 1 0%;
	padding: 4rem 1rem 1rem;
	display: flex;
	flex-direction: column;
}

#user {
	margin-right:110px;
}
#userIcon{
	padding-left: 10px;
}
#userIconBadge{
	padding-left: 75px;
}
#userIconBadge2{
	padding-left: 15px;
}
#userIconBadge3{
	padding-left: 35px;
}
#userIcon2{
	padding-left: 20px;
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

button {
	font-family: 'Zen Antique', serif ;
	font-weight: bold;
	font-size: 1.5rem;
	background-color: white;
	color: rgb(0, 0, 0);
	font-weight: 700;
	padding: 0.8rem 2rem;
	border-radius: 2rem;
	cursor: pointer;
	display: block;
	margin: auto auto 10px auto;
	border: none;
	width: 270px;
}

#last{
	margin-bottom: 85px;
}
#first{
	margin-top: 55px;
}

.button2 {
	margin: 15px auto 5px auto;
}


button:hover{
    color:azure;
    background: linear-gradient(90deg, #2171cf 0%, #13335c 100%);
    box-shadow: -10px -10px 15px rgba(192, 192, 192, 0.12),
                10px 10px 15px  rgba(0, 0, 0, 0.5);
}

p {
	color: black;
	font-weight: bold;
}

input {
	text-align: center;
	max-width: 220px;
	margin: auto;
}
#save {
	margin-bottom: 10px;
}
</style>