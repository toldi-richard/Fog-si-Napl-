<template>
<div class="background">
  <div class="container">
    <h1 >Felhasználók</h1>
    <nav id="topLine">
    <div class="input-group mb-3" >
      <p class="input-group-text"><span class="fa fa-user"></span> Azonosító:</p>
      <input id="searchInput" type="text" class="form-control" v-model="search"/>
    </div>
      <button id="reload" @click="reload()">&#128472;</button>
      <button type="button" class="btn btn-outline-primary" id="disk"
      data-toggle="modal" data-target="#myModal" @click="newUser()">&#128427;</button>
    </nav>
      <table class="table table-striped table-hover table-sm table-responsive">
        <thead class="thead-dark">
          <tr>
            <th>#</th>
            <th>Azonosító</th>
            <th>Szerepkör</th>
            <th>Email</th>
            <th>Műveletek</th>
          </tr>
        </thead>
        <tbody>
            <tr v-for="item in filteredUsers" :key="item.azonosito">
                <td><i class="fa fa-user"></i></td>
                <td>{{ item.azonosito }}</td>
                <td v-if="item.szerepkorID == 1">Adminisztrátor</td>
                <td v-if="item.szerepkorID == 2">Halőr</td>
                <td v-if="item.szerepkorID == 3">Horgász</td>
                <td>{{ item.email_cim }}</td>
                <td id="icons">
                  <span id="trash" @click="deleteAlert(item.azonosito)">&#128465;</span>
                  <span id="pen" data-toggle="modal" data-target="#myModal" @click="editAlert(item)">&#9998;</span>
                </td>
             </tr>
        </tbody>
      </table>
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
                    <input placeholder="123894" type="number" name="nev" class="form-control" v-model="licence"  required @change="licenceValid()" :disabled="newUsers">
                  </div>

                  <div class="input-group mb-3">
                    <select id="modal" v-model="role" required @change="editValidation">
                      <option selected disabled value="0">Kötelező választani!</option>
                      <option value="1">Adminisztrátor</option>
                      <option value="2">Halőr</option>
                      <option value="3">Horgász</option>
                    </select>
                  </div>

                  <div class="input-group mb-3">
                    <span class="input-group-text">Jelszó:</span>
                    <input @change="passValid(),editValidation()" placeholder="Jelszó" type="text" class="form-control" v-model="password"  required >
                  </div>

                  <div class="input-group mb-3">
                    <span class="input-group-text">Email:</span>
                    <input placeholder="email@gmail.com" type="email" class="form-control" v-model="email" @change="validation(),editValidation()" required>
                  </div>
                </div>
              </div>
              <nav id="modalButton">
                <button type="button" class="btn btn-primary" @click="saveUser()" data-dismiss="modal" :disabled="validatorSave"> Új mentése</button>
                <button type="button" class="btn btn-info" @click="updateUser()" data-dismiss="modal" :disabled="validatorUpdate"> Módosítás</button>
                <button id="button" type="button" class="btn btn-danger" data-dismiss="modal" @click="cancel()">Vissza</button>
              </nav>
            </div>
          </div>
        </div>
        <!-- Modal -->
    </div>
  </div>
</div>
</template>
<script>
import axios from 'axios';

export default {
    name: 'Felhasznalok',
  data() {
	return{
    newUsers: true,
		search:'',
		listOfUsers:[],
    searchPlaces:'',
    licence:null,
    modalHeader:null,
    role:null,
    password:null,
    email:null,
    originLicence:null,
    validator:null,
    validatorSave:true,
    validatorUpdate:true,
    regEmail: /[A-z\u00C0-\u00FF0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/,
    regLicence: /^[1-9]{1}\d{4,5}$/,
    regPassword: /^[A-z\u00C0-\u00FF0-9._%&@#:;$+-/]{8,}$/,
    // minimum 8 karakter
    }
  },
  created() {
      axios.get('http://localhost:5000/api/Felhasznalok/Mind')
      .then(response=>{response.data;this.listOfUsers=response.data;})
      .catch(error=>console.log(error))
  },
  computed: {
      filteredUsers: function() {
        return this.listOfUsers.filter((item) => {
          return item.azonosito.toString().match(this.search)
        });
      }
    },
    methods: {
      cancel(){
        this.validator = 0 ,
        this.validatorSave = true ,
        this.validatorUpdate=true,
        this.modalHeader=null 
      },
      reload(){
        this.updateUsers(),
        this. cancel(),
        this.newUsers= false
      },
      updateUsers(){
        axios.get('http://localhost:5000/api/Felhasznalok/Mind')
          .then(response=>{response.data;this.listOfUsers=response.data;})
          .catch(error=>console.log(error))
      },

      deleteAlert(azonosito) {
         this.$swal.fire({
            title: 'Biztosan töröli a '+azonosito +' felhasználót?',
            showDenyButton: true,
            confirmButtonText: 'Igen',
            denyButtonText: `Nem`,
          }).then((result) => {
            if (result.isConfirmed) {
              this.$swal.fire('Törölve!', '', 'success'),
              this.deleteUser(azonosito),
              this.updateUsers()
            } else if (result.isDenied) {
              this.$swal.fire('Visszalépett', 'A törlés nem lett végrehajtva!', 'info')}})},

      deleteUser(azonosito){
      axios.delete('http://localhost:5000/api/Felhasznalok/'+ azonosito)
       .then(this.showAlertTorles())},

      newUser(){
        this.newUsers= false,
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
        .then(this.showAlert());

      },

      showAlert() {
        this.$swal.fire({
        icon: 'success',
        title: 'Sikeres mentés!',
        text: 'Az új felhasználó hozzáadása sikerült! Frissítse a táblázatot a floppy melletti gombbal!',
        confirmButtonText: 'Ok'})
        },

      showAlertTorles() {
        this.$swal.fire({
        icon: 'success',
        title: 'Sikeres törlés!',
        text: 'A felhasználó törlése sikerült! Frissítse a táblázatot a floppy melletti gombbal!',
        confirmButtonText: 'Ok'});
        },

      editAlert(item){
        this.newUsers = true,
        this.modalHeader = "Felhasználó adatainak módosítása",
        this.originLicence=item.azonosito,
        this.licence=item.azonosito,
        this.role=item.szerepkorID,
        this.password=item.jelszo,
        this.email=item.email_cim;
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
      }).then(this.showAlertUpdate())},

      showAlertUpdate() {
        this.$swal.fire({
        icon: 'success',
        title: 'Sikeres módosítás!',
        text: 'A felhasználó adatainak módosítása sikerült! Frissítse a táblázatot a floppy melletti gombbal!',
        confirmButtonText: 'Ok'})},

      alert() {
        this.$swal.fire({
        icon: 'error',
        title: 'A művelet megtagadva!',
        text: 'Kérem ellenőrizze a megadott értékeket!',
        confirmButtonText: 'Ok'});
        },

      alertLicence() {
        this.$swal.fire({
        icon: 'error',
        title: 'Rossz engedélyszám!',
        text: 'Kérem ellenőrizze a megadott engedély számot!',
        confirmButtonText: 'Ok'});
        this.validator=0
        },
      alertEmail() {
        this.$swal.fire({
        icon: 'error',
        title: 'Rossz email cím!',
        text: 'Kérem ellenőrizze a megadott címet!',
        confirmButtonText: 'Ok'});
        this.validator=0
        },
      alertPassword() {
        this.$swal.fire({
        icon: 'error',
        title: 'Gyenge jelszó!',
        text: 'Kérem legalább 8 karaktert adjon meg!',
        confirmButtonText: 'Ok'});
        this.validator=0
        },

        licenceValid(){
          if (!this.regLicence.test(this.licence)) {this.alertLicence()}
          if (this.regLicence.test(this.licence) && this.regEmail.test(this.email) && this.regPassword.test(this.password) && this.role != 0) {
          this.validator = 0 }},

        passValid(){
          if (!this.regPassword.test(this.password)) {this.alertPassword()}
          if (this.regLicence.test(this.licence) && this.regEmail.test(this.email) && this.regPassword.test(this.password) && this.role != 0) {
          this.validator = 0 }},

      editValidation(){
        if(this.regLicence!=null && this.email!=null && this.password!=null && this.modalHeader == "Felhasználó adatainak módosítása")
        {
          this.validatorUpdate = false
        }
      },

      validation(){
        if(this.modalHeader == "Felhasználó adatainak módosítása")
        {
        this.validator=2
        } else {this.validator=1}

        if (this.regLicence.test(this.licence) && this.regEmail.test(this.email) && this.regPassword.test(this.password) && this.role != 0) {
          if (this.validator == 1) {
            this.validatorSave = false
          } else {this.validatorUpdate = false}
        } else {
          if(!this.regLicence.test(this.licence) || this.licence == null) {this.alertLicence() }
          if(!this.regPassword.test(this.password) && this.password != null) {this.alertPassword() }
          if(!this.regEmail.test(this.email) || this.email ==  null) 
          {
            this.alertEmail()
          }
           this.cancel()
        }
      }
  }
}
</script>

<style scoped>
@media only screen and (max-width: 660px) {
.background {
  background-image: none !important;
	}
}
select {
  width: 520px;
}

#topLine{
  display: flex;
  justify-content: space-between;
}

#modalButton{
  display: flex;
  justify-content: space-evenly;
}

.background {
	transition: 1000ms;
	background-image: url('~@/assets/wave.jpg');
	background-size: cover;
	min-height: 100vh;
	padding-top: 50px;
}


#icons{
  display: flex;
  justify-content: space-evenly;
  max-width: 200px;
}
#trash {
  padding-left:12px;
  border-radius: 10px;
  font-weight: bold;
  font-size: 3rem;
  width: 80px;
  color:rgb(252, 60, 60);
  border:1px solid rgb(252, 60, 60);
}

#trash:hover{
  width: 100px;
  color: white;
  background-color: rgb(252, 60, 60);
  font-size: 3.5rem;
}
#reload {
  font-weight: bold;
  font-size: 50px;
  max-height: 80px;
  width: 90px;
  border-radius: 10px;
  border: 1px solid blue;
  color: blue;
  background-color: rgba(0, 0, 255, 0);
}

#reload:hover{
  width: 100px;
  max-height: 90px;
  color: white;
  background-color: rgb(0, 0, 255);
  font-size: 3.5rem;
}

#pen {
  border-radius: 10px;
  font-size: 3rem;
  width: 80px;
  color:rgb(39, 227, 5);
  border:1px solid rgb(39, 227, 5);
}
#pen:hover{
  width: 100px;
  color: white;
  background-color:  rgb(39, 227, 5);
  font-size: 3.5rem;
}

#disk {
  font-weight: bold;
  font-size: 50px;
  max-height: 80px;
  border-radius: 10px;
  width: 90px;
}
#disk:hover{
  width: 100px;
  max-height: 90px;
  color: white;
  font-size: 3.5rem;
}

i {
  font-size: 2rem;
}

span {
  padding-right: 5px; 
}

td {
  width: 700px;
}

p {
  font-weight: bold;
  background: #13335c;
  color: white;
}

table {
  max-height: 790px;
  background-color:rgba(255, 255, 255, 0.976);
  color: black;
  font-size: 2rem;
}

.table-hover tbody tr:hover td, .table-hover tbody tr:hover th {
  background-color:  #13335c;
  font-weight: bold;
  color: rgb(255, 255, 255);
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
	padding: 3.5rem 0rem 2rem 0rem;
  white-space:normal;
  word-break: break-all;
  text-align:center;
}

select {
	width: 380px;
	border: 1px solid rgba(128, 128, 128, 0.484);
	height: 40px;
	text-align: center;
	color: rgba(68, 68, 68, 0.948);
}
</style>