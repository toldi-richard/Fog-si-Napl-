<template>
<div class="background">
  <div class="container">
	<h1>Horgászatra kijelölt vízterek</h1>
    <div class="input-group mb-3" id="search">
      <p class="input-group-text">Helyszín:</p>
      <input id="searchInput" type="text" class="form-control" v-model="search"/>
    </div>
      <table class="table table-striped table-hover table-sm table-responsive">
        <thead class="thead-dark">
          <tr>
            <th>#</th>
            <th>Megnevezés:</th>
            <th>Víztérkód:</th>
          </tr>
        </thead>
        <tbody>
            <tr v-for="item in filteredPlaces" :key="item.id">
                <td><i class="fa fa-water"></i></td>
                <td>{{ item.vizterulet_neve }}</td>
                <td>{{ item.vizterkod }}</td>
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
		listOfPlaces:[],
    searchPlaces:''
	}
  },
  created() {
	axios.get('http://localhost:5000/api/Helyszinek').then(response=>{response.data;this.listOfPlaces
=response.data;}).catch(error=>console.log(error))
  },
  computed: {
      filteredPlaces: function() {
        return this.listOfPlaces.filter((item) => {
          return item.vizterulet_neve.match(this.search)
        });
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

.row {
  margin-bottom: 20px;
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