<template>
<div class="background">
  <div class="container">
    <h1>Fogásaim</h1>
    <div class="input-group mb-3" id="search">
      <p class="input-group-text"> Helyszín:</p>
      <input id="searchInput" type="text" class="form-control" v-model="search"/>
    </div>

  <div class="row">

    <!-- Card -->
      <div id="card" class="card col-sm-12 col-md-6 col-lg-4"
      type="button" data-toggle="modal" data-target="#modal" >
        <div class="card-body">
          <i class="fa fa-fish"/>
          <h5 class="card-title">Kifogott halak száma:</h5>
          <h2 class="card-subtitle mb-2">{{ numberOfCaughtFish }}</h2>
        </div>
      </div>
    <!-- Card -->

    <!-- Card -->
      <div id="card" class="card col-sm-12 col-md-6 col-lg-4" 
      type="button" data-toggle="modal" data-target="#modal">
        <div class="card-body">
          <i class="fa fa-weight-hanging"/>
          <h5 class="card-title">Kifogott halak súlya:</h5>
          <h2 class="card-subtitle mb-2">{{ weightOfCaughtFish }}</h2>
        </div>
      </div>
    <!-- Card -->

    <!-- Card -->
      <div id="card" class="card col-sm-12 col-md-6 col-lg-4"
      type="button" data-toggle="modal" data-target="#modal" >
        <div class="card-body">
          <i class="fa fa-fish"/>
          <h5 class="card-title">Leggyakrabban fogott hal:</h5>
          <h2 class="card-subtitle mb-2">{{ mostCommonlyCaughtFishType[0] }}</h2>
        </div>
      </div>
    <!-- Card -->
    </div>
      <table class="table table-striped table-hover table-sm table-responsive">
        <thead class="thead-dark">
          <tr>
            <th>#</th>
            <th>Időpont:</th>
            <th>Helyszín:</th>
            <th>Halfaj:</th>
            <th>Súly:</th>
          </tr>
        </thead>
        <tbody>
            <tr v-for="item in filteredCatches" :key="item.id">
                <td><i class="fa fa-clipboard"></i></td>
                <td>{{ item.datum_idopont }}</td>
                <td>{{ item.helyszin }}</td>
                <td>{{ item.halfaj }}</td>
                <td>{{ item.suly }}</td>
             </tr>
        </tbody>
      </table>
  </div>
</div>
</template>

<script>
import axios from 'axios';

export default {
    name: 'Fogasaim',
  data() {
	return{
		id: localStorage.getItem("id"),
		identifierNumber:'',
		search:'',
		listOfCatches:[],
    searchPlaces:'',
    numberOfCaughtFish:null,
    weightOfCaughtFish:null,
    mostCommonlyCaughtFishType:''
	}
  },
  created() {
      axios.get('http://localhost:5000/api/Fogasok/'+this.id.toString()).then(response=>{response.data;this.listOfCatches=response.data;}).catch(error=>console.log(error))

      axios.get('http://localhost:5000/api/Fogasok/kifogotthal_szama/'+this.id.toString()).then(response=>{response.data;this.numberOfCaughtFish=response.data+' db';}).catch(error=>console.log(error))
      
      axios.get('http://localhost:5000/api/Fogasok/kifogotthal_sulya/'+this.id.toString()).then(response=>{response.data;this.weightOfCaughtFish=response.data+' kg';}).catch(error=>console.log(error))

      axios.get('http://localhost:5000/api/Fogasok/leggyakrabban_kifogott_halfaj/'+this.id.toString()).then(response=>{response.data;this.mostCommonlyCaughtFishType=response.data;}).catch(error=>console.log(error))
  },
  computed: {
      filteredCatches: function() {
        return this.listOfCatches.filter((item) => {
          return item.helyszin.match(this.search)
        });
      }
    },
}
</script>

<style scoped>


.row {
  margin-bottom: 20px;
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
.background {
	transition: 1000ms;
	background-image: url('~@/assets/wave.jpg');
	background-size: cover;
	min-height: 100vh;
	padding-top: 50px;
	}

i {
  font-size: 2rem;
}

td{
  width: 700px;
}

p {
  font-weight: bold;
  background-color: #13335c;
  color: white;
}


table {
  max-height: 790px;
  background-color:rgba(255, 255, 255, 0.976);
  color: black;
  font-size: 1rem;
}

.table-hover tbody tr:hover td, .table-hover tbody tr:hover th {
  background-color:  #13335c;
  font-weight: bold;
  color: rgb(255, 255, 255);
  font-size: 1.3rem;
}

#search {
	padding-top: 50px;
}

#searchInput {
  max-width: 210px;
}


h1 {
	font-family: 'Zen Antique', serif;
	font-size: 2.6rem;
	font-weight: bold;
	text-transform: uppercase;
	padding: 4rem 0rem 0rem 0rem;
  white-space:normal;
  word-break: break-all;
  text-align:center;
}
</style>