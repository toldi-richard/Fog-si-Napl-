<template>
<div id="background">
  <div class="container">
    <h1>Horgászok</h1>
    <!-- Card -->
    <div class="input-group mb-3" id="search">
      <p class="input-group-text"><span class="fa fa-user"></span>Engedély:</p>
      <input id="searchInput" type="text" class="form-control" v-model="search"/>
    </div>
    <div class="row">
      <div id="card" class="card col-sm-12 col-md-6 col-lg-4" v-for="item in filteredUsers" :key="item.azonosito"
      type="button" data-toggle="modal" data-target="#modal" @click="getCatches(item.azonosito)">
        <div class="card-body">
          <i class="fa fa-user"/>
          <h5 class="card-title">{{ item.azonosito }}</h5>
          <h6 class="card-subtitle mb-2">{{ item.email_cim }}</h6>
        </div>
      </div>
    </div>
    <!-- Modal -->
            <div class="modal fade" id="modal" tabindex="-1" role="dialog" aria-labelledby="modalitle" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h2 class="modal-title" id="modalTitle">Fogások</h2>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="false">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <table class="table table-striped table-hover table-sm table-responsive">
                <thead class="thead-dark">
                  <tr>
                    <th>#</th>
                    <th>Időpont: </th>
                    <th>Helyszín: </th>
                    <th>Halfaj: </th>
                    <th>Súly:</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="item in catches" :key="item.azonosito">
                    <td><i class="fa fa-clipboard"></i></td>
                    <td>{{ item.datum_idopont }}</td>
                    <td>{{ item.helyszin }}</td>
                    <td>{{ item.halfaj }}</td>
                    <td>{{ item.suly }}</td>
                  </tr>
                </tbody>
              </table>
              </div>
              <div class="modal-footer">
                <button id="button" type="button" data-dismiss="modal">Bezár</button>
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
    name: 'Felhasznalok',
  data() {
	return{
    catches:[],
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
      getCatches(azonosito){
        axios.get('http://localhost:5000/api/Fogasok/'+ azonosito.toString())
             .then(response => {response.data;
             this.catches = response.data;})
             .catch(error => console.log(error))
      },
      updateTable(){
        axios.get('http://localhost:5000/api/Felhasznalok')
        .then(response => {this.listOfUsers = response.data;})
        .catch(error => console.log(error))
      },
  }
}
</script>

<style scoped>
@media only screen and (max-width: 560px) {
#background {
  background-image: none !important;
	}
}

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
  background: linear-gradient(90deg, #2171cf 0%, #13335c 100%);
  color: white;
  box-shadow: 6px 5px 12px -2px rgba(0,0,0,0.62);
}

#card:hover{
    color:azure;
    background: linear-gradient(90deg, #9ebd13 0%, #008552 100%);
    box-shadow: -10px -10px 15px rgba(192, 192, 192, 0.12),
                10px 10px 15px  rgba(0, 0, 0, 0.5);
}

i {
  font-size: 2rem;
}


table {
  max-height: 790px;
  background-color:rgba(255, 255, 255, 0.976);
  color: black;
  font-size: 1rem;
}

.table-hover tbody tr:hover td, .table-hover tbody tr:hover th {
  background-color: #13335c;
  font-weight: bold;
  color: rgb(255, 255, 255);
  font-size: 1.3rem;
}


p {
  font-weight: bold;
  background-color: #13335c;
  color: white;
}

span {
  margin-right: 10px;
}

#search {
  padding-top: 50px;
}

#searchInput {
  max-width: 150px;
  max-height: 40px;
  text-align: center;
  font-weight: bold;
  font-size: 2rem;
}

h1 {
	font-family: 'Zen Antique', serif;
	font-size: 2.6rem;
	font-weight: bold;
	text-transform: uppercase;
	padding: 6.5rem 0rem 0rem 0rem;
  white-space:normal;
  word-break: break-all;
  text-align:center;
}
#button{
  background-image: linear-gradient(
		to bottom right,
		rgb(255, 255, 255) 0%,
		hsl(0, 0%, 57%) 100%);
	color: rgb(0, 0, 0);
	padding: 1.2rem;
	border-radius: 2rem;
	cursor: pointer;
	border: 1px solid rgb(233, 233, 233);
}
#button:hover{
  color:azure;
  background: rgba(196, 196, 196, 0.671);
	box-shadow: -10px -10px 15px rgba(192, 192, 192, 0.12),
				10px 10px 15px  rgba(0, 0, 0, 0.5);
}
</style>