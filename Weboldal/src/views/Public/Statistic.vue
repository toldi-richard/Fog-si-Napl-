<template>
<div id="background">
  <div class="container">
    <h1 >Statisztika</h1>

  <div class="row">
    <!-- Card -->
      <div id="card" class="card col-sm-12 col-md-6 col-lg-4"
      type="button" data-toggle="modal" data-target="#modal">
        <div class="card-body">
          <i class="fa fa-user"/>
          <h5 class="card-title">Felhasználók száma:</h5>
          <h2 class="card-subtitle mb-2">{{ numberOfUsers }}</h2>
        </div>
      </div>
    <!-- Card -->

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
        mostCommonlyCaughtFishType:'',
        numberOfUsers:null,
	}
  },
  created() {
    axios.get('http://localhost:5000/api/Felhasznalok/szama').then(response=>{response.data;this.numberOfUsers=response.data;}).catch(error=>console.log(error))

	axios.get('http://localhost:5000/api/Fogasok/0').then(response=>{response.data;this.listOfCatches=response.data;}).catch(error=>console.log(error))

	axios.get('http://localhost:5000/api/Fogasok/kifogotthal_szama/0').then(response=>{response.data;this.numberOfCaughtFish=response.data+' db';}).catch(error=>console.log(error))
      
  axios.get('http://localhost:5000/api/Fogasok/kifogotthal_sulya/0').then(response=>{response.data;this.weightOfCaughtFish=response.data+' kg';}).catch(error=>console.log(error))

  axios.get('http://localhost:5000/api/Fogasok/leggyakrabban_kifogott_halfaj/0').then(response=>{response.data;this.mostCommonlyCaughtFishType=response.data;}).catch(error=>console.log(error))
  },
  computed: {
      filteredCatches: function() {
        return this.listOfCatches.filter((item) => {
          return item.helyszin.match(this.search)
        });
      }
    }
}
</script>

<style scoped>
@media only screen and (max-width: 660px) {
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

h1 {
	font-family: 'Zen Antique', serif;
	font-size: 2.6rem;
	font-weight: bold;
	text-transform: uppercase;
	padding: 6.5rem 0rem 4rem 0rem;
  white-space:normal;
  word-break: break-all;
  text-align:center;
}

</style>