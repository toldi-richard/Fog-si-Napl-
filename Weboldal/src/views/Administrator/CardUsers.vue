<template>





<!-- Fejlesztés alatt áll az oldal, még nem elérhető a menüből és a halőr oldala van csak rajta, hiányoznak a műveleti gombok még a kártyákról... -->





<div id="background">
  <div class="container">
    <h1 >Felhasználók</h1>
    <nav id="topLine">
    <div class="input-group mb-3" >
      <p class="input-group-text"><span class="fa fa-user"></span> Azonosito:</p>
      <input id="searchInput" type="text" class="form-control" v-model="search"/>
    </div>
      <button type="button" id="disk"
      data-toggle="modal" data-target="#myModal" @click="newUser(item.azonosito)">&#128427;</button>
    </nav>
    <div class="row">
      <div id="card" class="card col-sm-12 col-md-6 col-lg-4" v-for="item in filteredUsers" :key="item.azonosito">
        <div class="card-body">
          <h5 class="card-title">{{ item.azonosito }}</h5>
          <h6 class="card-subtitle mb-2">{{ item.email_cim }}</h6>
        </div>
      </div>
    </div>
      <!-- Modal -->
     <div class="modal fade" id="myModal">
        <div class="modal-dialog modal-lg modal-dialog-centered">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="ModalLabel">{{ modalHeader}}</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="d-flex justify-content-center">
                <div class="p-2 w-50 bd-highlight">

                  <div class="input-group mb-3">
                    <span class="input-group-text">Engedély:</span>
                    <input placeholder="123894" type="text" name="nev" class="form-control" v-model="licence" @change="licenceValid" required>
                  </div>

                  <div class="input-group mb-3">
                    <select id="modal" v-model="role" required>
                      <option selected disabled value="0">Szerepkör</option>
                      <option value="1">Adminisztrátor</option>
                      <option value="2">Halőr</option>
                      <option value="3">Felhasználó</option>
                    </select>
                  </div>

                  <div class="input-group mb-3">
                    <span class="input-group-text">Jelszó:</span>
                    <input placeholder="Jelszó" type="text" class="form-control" v-model="password" @change="passwordValid" required>
                  </div>

                  <div class="input-group mb-3">
                    <span class="input-group-text">Email:</span>
                    <input placeholder="email@gmail.com" type="text" class="form-control" v-model="email" @change="emailValid" required>
                  </div>
                </div>
              </div>
              <nav id="modalButton">
                <button type="button" class="btn btn-primary" @click="saveUser" data-dismiss="modal" >Mentés</button>
                <button type="button" class="btn btn-info" @click="updateUser" data-dismiss="modal" >Módosítás</button>
                <button id="button" type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
              </nav>
            </div>
          </div>
        </div>
    </div>
  </div>
</div>


<!-- Fejlesztés alatt áll az oldal, még nem elérhető a menüből és a halőr oldala van csak rajta, hiányoznak a műveleti gombok még a kártyákról... -->


</template>

<script>
import axios from 'axios';

export default {
    name: 'Felhasznalok',
  data() {
	return{
		search:'',
		listOfUsers:[],
    searchPlaces:'',
    licence:null,
    modalHeader:null,
    licenceValid:false,
    role:null,
    password:null,
    passwordVaéod:null,
    email:null,
    emailValid:null,
    originLicence:null	
    }
  },
  created() {
      axios.get('http://localhost:5000/api/Felhasznalok/').then(response=>{response.data;this.listOfUsers=response.data;}).catch(error=>console.log(error))
  },
  computed: {
      filteredUsers: function() {
        return this.listOfUsers.filter((item) => {
          return item.azonosito.toString().match(this.search)
        });
      }
    },
    methods: {
      updateTable(){
        axios.get('http://localhost:5000/api/Felhasznalok')
        .then(response => {this.listOfUsers = response.data;})
        .catch(error => console.log(error))
      },
      deleteAlert(azonosito) {
         this.$swal.fire({
            title: 'Do you want to save the changes?',
            showDenyButton: true,
            confirmButtonText: 'Törlés',
            denyButtonText: `Mégse`,
          }).then((result) => {
            if (result.isConfirmed) {
              this.$swal.fire('Törölve!', '', 'success'),
              this.deleteUser(azonosito)
            } else if (result.isDenied) {
              this.$swal.fire('Visszalépett', 'A törlés nem lett végrehajtva!', 'info')
            }
          })
      },

      deleteUser(azonosito){
      axios.delete('http://localhost:5000/api/Felhasznalok/'+ azonosito)
       .then(this.updateTable(),this.showAlertTorles())
       },

      newUser(){
          this.modalHeader = "Új felhasznaló hozzáadás",
          this.licence=null,
          this.role="0",
          this.password=null,
          this.email=null
       },

      saveUser(){
        axios.post('http://localhost:5000/api/Felhasznalok', {
          azonosito:this.licence,
          szerepkorID:this.role,
          jelszo:this.password,
          email_cim:this.email
        })
        .then(this.updateTable(),
        this.showAlert());
      },

      showAlert() {
        this.$swal.fire({
        icon: 'success',
        title: 'Sikeres mentés!',
        text: 'Az új felhasználó hozzáadása sikerült!',
        confirmButtonText: 'Ok'});
        },

      showAlertTorles() {
        this.$swal.fire({
        icon: 'success',
        title: 'Sikeres törlés!',
        text: 'A felhasználó törlése sikerült!',
        confirmButtonText: 'Ok'});
        },

      editAlert(item){
        this.modalTitle = "Felhasználó adatainak módosítása",
        this.originLicence=item.azonosito,
        this.licence=item.azonosito,
        this.role=item.szerepkorID,
        this.password=item.jelszo,
        this.email=item.email_cim
      },
      
      // Azonosító nem módosítható mert az a primary key
      // ezért érdemes id-t primaryként adni mindennek
      // ha változik az engedélye, akkor azt is lehessen változtatni
      // így csak a törlés és újra felvitel marad...ha azt akarjuk változtatni
      updateUser(){
        axios.put('http://localhost:5000/api/Felhasznalok/'+this.originLicence, {
          azonosito:this.licence,
          szerepkorID:this.role,
          jelszo:this.password,
          email_cim:this.email
      }).then(this.updateTable(),
        this.showAlertUpdate());},

      showAlertUpdate() {
        this.$swal.fire({
        icon: 'success',
        title: 'Sikeres módosítás!',
        text: 'A felhasználó adatainak módosítása sikerült!',
        confirmButtonText: 'Ok'});
        },
  }
}
</script>

<style scoped>
#background {
	background-image: url('~@/assets/wave.jpg');
	background-size: cover;
  min-height: 100vh;
}

#card{
  margin: auto;
  max-width: 250px;
  border-radius: 10px;
  color: black;
  font-weight: bold;
  box-shadow: 6px 5px 12px -2px rgba(0,0,0,0.62);
}

#card:nth-child(even){
  background: rgba(0, 0, 0, 0.689);
  color: white;
  box-shadow: 6px 5px 12px -2px rgba(0,0,0,0.62);
}

#card:hover{
  color: black;
  box-shadow: 6px 5px 24px -2px rgb(0, 0, 0);
  background: linear-gradient(135deg, rgb(255, 255, 255) 0%, rgba(159,159,159,0.5747871517027864) 100%);
}

select {
  color: rgb(130, 130, 130);
  text-align: center;
  width: 220px;
  height: 40px;
  border: 1px solid rgba(169, 169, 169, 0.866);
  border-radius: 5px;
}
#topLine{
  display: flex;
  justify-content: space-between;
}
#modalButton{
  display: flex;
  justify-content: space-evenly;
}

#icons{
  display: flex;
  justify-content: space-evenly;
  max-width: 150px;
}
#trash {
  padding-left:12px;
  border-radius: 10px;
  font-weight: bold;
  font-size: 3rem;
  color:rgb(252, 60, 60);
  border:1px solid rgb(252, 60, 60);
}

#trash:hover{
  color: white;
  background-color: rgb(252, 60, 60);
  font-size: 3.5rem;
}

#pen {
  border-radius: 10px;
  font-size: 3rem;
  color:rgb(39, 227, 5);
  border:1px solid rgb(39, 227, 5);
}
#pen:hover{
  color: white;
  background-color:  rgb(39, 227, 5);
  font-size: 3.5rem;
}

#disk {
  background-color: rgba(190, 246, 244, 0);
  border: 1px solid rgba(255, 255, 255, 0);
  color: rgb(0, 0, 0);
  font-weight: bold;
  font-size: 55px;
  border-radius: 100px;
}
#disk:hover{
  color: white;
  font-size: rem;
}

/* 1920 x 1080 Full High Definition (FHD) */
.container{
  margin: auto;
  max-width: 1920px;
  min-height: 1080px;
  background: linear-gradient(135deg, rgb(255, 255, 255) 0%, rgba(159,159,159,0.5747871517027864) 100%);
}

i {
  font-size: 2rem;
}

span {
  padding-right: 5px; 
}

td{
  width: 700px;
}

p {
  font-weight: bold;
  background: rgba(0, 0, 0, 0.689);
  color: white;
}

table {
  max-height: 790px;
  background-color:rgba(255, 255, 255, 0.976);
  color: black;
  font-size: 1rem;
}

.table-hover tbody tr:hover td, .table-hover tbody tr:hover th {
  background-color: rgba(74, 230, 254, 0.368);
  font-weight: bold;
  font-size: 1.3rem;
}

#searchInput {
  max-width: 210px;
  height: 70px;
  text-align: center;
  font-weight: bold;
  font-size: 2rem;
}

h1 {
	font-family: 'Zen Antique', serif;
	font-size: 2.6rem;
	font-weight: bold;
	text-transform: uppercase;
	padding: 8rem 0rem 8rem 0rem;
  white-space:normal;
  word-break: break-all;
  text-align:center;
}
</style>