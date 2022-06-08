<template>
<div><Navigation/></div>
<div id="background" @click="getData()">
  <main id="main">
    <section class="form">
      <span v-if="identifierNumber && !wrongIdentifierNumber"><List :listdata="listOfCatches"/></span>
      <form>
        <div class="form-group">
          <h1>Fogás regisztrálása</h1>
          <i class="fa fa-id-badge"></i><label> Horgászengedély:</label>
          <p id="fishingLicence">{{identifierNumber}}</p>
          <p class="error" v-if="wrongIdentifierNumber"><span class="fas fa-exclamation-circle"></span> Az azonosító helytelen!</p>

          <i class="fa fa-location-arrow"></i> 
          <label> Helyszín:</label>
              <select name="fishpond" required v-model="fishpond" @change="catchReport()">
                <option disabled value="">Kötelező kitölteni!</option>
                <option v-for="item in lakes" :key="item.helyszinID"
                :value="item.helyszinID">
                  {{item.vizterulet_neve}}</option>
              </select>


          <br>
          <i class="fa fa-location-arrow"></i> 
          <label> Horgászhely:</label>
          <input name="fishpondNumber" type="text" class="form-input" v-model.trim="fishpondNumber"  placeholder="pl.: 1 vagy A/2"
          @change="verifyFishpondNumber()">
          <p class="error" v-if="wrongFishpondNumber"><span class="fas fa-exclamation-circle"></span> A megadott horgászhely helytelen!</p>

          <br>
          <i class="fa fa-calendar"></i> 
          <label> Időpont:</label>
          <p id="date">{{catchDateTime}}</p>
          <!-- <p class="error" v-if="wrongCatchDateTime"><span class="fas fa-exclamation-circle"></span> A megadott időpont helytelen!</p> -->

          <br>
          <i class="fa fa-fish"></i> 
          <label> Halfaj:</label>
          <select name="fish" required v-model="fish" @change="catchReport()">
                <option value="" >Kötelező kitölteni!</option>
                <option value="ponty">ponty</option>
                <option value="amur">amur</option>
                <option value="márna">márna</option>
                <option value="compó">compó</option>
                <option value="harcsa">harcsa</option>
                <option value="csuka">csuka</option>
                <option value="fogassüllő">fogas-süllő</option>
                <option value="kősüllő">kő-süllő</option>
                <option value="balin">balin</option>
                <option value="sebes pisztráng">sebes pisztráng</option>
                <option value="kecsege">kecsege</option>
                <option value="széles kárász">széles kárász</option>
                <option value="angolna">angolna</option>
                <option value="menyhal">menyhal</option>
                <option value="sügér">sügér</option>
                <option value="domolykó">domolykó</option>
                <option value="garda">garda</option>
                <option value="egyéb keszegfélék">egyéb keszegfélék</option>
                <option value="ezüstkárász">ezüstkárász</option>
                <option value="busák">busák</option>
                <option value="törpeharcsák">törpeharcsák</option>
                <option value="egyéb">egyéb halfajta</option>
          </select>

          <br>
          <i class="fa fa-weight-hanging"/>
          <label> Súly:</label>
              <select name="weightOfFish" id="last" required v-model="weightOfFish" @change="catchReport()">
                <option value="" >Kötelező kitölteni!</option>
                <option value="0.5">0.5 Kg</option>
                <option value="1">1 Kg</option>
                <option value="1.5">1.5 Kg</option>
                <option value="2">2 Kg</option>
                <option value="2.5">2.5 Kg</option>
                <option value="3">3 Kg</option>
                <option value="3.5">3.5 Kg</option>
                <option value="4">4 Kg</option>
                <option value="4.5">4.5 Kg</option>
                <option value="5">5 Kg</option>
                <option value="5.5">5.5 Kg</option>
                <option value="6">6 Kg</option>
                <option value="6.5">6.5 Kg</option>
                <option value="7">7 Kg</option>
                <option value="7.5">7.5 Kg</option>
                <option value="8">8 Kg</option>
                <option value="8.5">8.5 Kg</option>
                <option value="9">9 Kg</option>
                <option value="9.5">9.5 Kg</option>
                <option value="10">10 Kg</option>
                <option value="10.5">10.5 Kg</option>
                <option value="11">11 Kg</option>
                <option value="11.5">11.5 Kg</option>
                <option value="12">12 Kg</option>
                <option value="12.5">12.5 Kg</option>
                <option value="13">13 Kg</option>
                <option value="13.5">13.5 Kg</option>
                <option value="14">14 Kg</option>
                <option value="14.5">14.5 Kg</option>
                <option value="15">15 Kg</option>
                <option value="15.5">15.5 Kg</option>
                <option value="16">16 Kg</option>
                <option value="16.5">16.5 Kg</option>
                <option value="17">17 Kg</option>
                <option value="17.5">17.5 Kg</option>
                <option value="18">18 Kg</option>
                <option value="18.5">18.5 Kg</option>
                <option value="19">19 Kg</option>
                <option value="19.5">19.5 Kg</option>
                <option value="20">20 Kg</option>
                <option value="20.5">20.5 Kg</option>
                <option value="21">21 Kg</option>
          </select>
          <input data-toggle="modal" data-target="#confirm"
           id="cacthSubmit" type="button" value="&#10004;   Leadás" v-if="cacthSubmit" @click="send()">
        </div>
      </form>
    </section>

    <!-- Modal -->
    <div class="modal fade" id="confirm" tabindex="-1" role="dialog" aria-labelledby="confirm" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLongTitle">Megerősítés</h5>
            <button type="button" class="close" data-dismiss="modal" @click="back" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
      <!-- Megadott adatok táblázatos formában történő kilistázása és azok végleges leadása -->
      <form id="confirm-form">
          <h1>Helyesek az adatok?</h1>
          <!-- Adatok táblázatos kilistázása -->
          <table>
              <tr>
                  <td><i class="fa fa-id-badge"></i></td>
                  <td><p>{{identifierNumber}}</p></td>
              </tr>
              <tr>
                  <td><i class="fa fa-location-arrow"></i></td>
                  <td><p>{{fishpondName}}</p></td>
              </tr>
              <tr>
                  <td><i class="fa fa-location-arrow"></i></td>
                  <td><p>{{fishpondNumber}}</p></td>
              </tr>
              <tr>
                  <td><i class="fa fa-calendar"></i> </td>
                  <td><p>{{catchDateTime}}</p></td>
              </tr>
              <tr>
                  <td><i class="fa fa-fish"></i></td>
                  <td><p>{{fish}}</p></td>
              </tr>
              <tr>
                  <td><i class="fa fa-weight-hanging"></i></td>
                  <td><p>{{weightOfFish}} Kg</p></td>
              </tr>
              <tr>
                  <th><input id="confirmNO" type="button" data-dismiss="modal" value="&#10008;  Nem" @click="back"></th>
                  <th><input id="confirmYES" type="button" data-dismiss="modal" value="&#10004;  Igen" @click="onCreatePost();backSend()"></th>
              </tr>
          </table>
      </form>
          </div>
        </div>
      </div>
    </div>
  </main>
</div>
</template>

<script>
import axios from 'axios';
import List from '../../components/List.vue';
import { format } from 'date-fns'

export default {
  name: 'Post',
  components: {
    List
  },
  data() {
    return {
            lakes:[],
            email: localStorage.getItem("email"),

            // Validálásokhoz REGEX, fejlesztések miatt nincs minde használva már
            regexIdentifierNumber: /^[1-9]{1}\d{5}$/,
            wrongIdentifierNumber: false,
            regexFishpond: /^[A-z\u00C0-\u00FF]?\/?\d{1,2}$\/?[A-z\u00C0-\u00FF]?$/,
            wrongFishpondNumber: false,
            wrongCatchDateTime: false,
            cacthSubmit: false,

            identifierNumber: localStorage.getItem("id"),
            fishpond: "",
            fishpondNumber: "",

            catchDateTimeOrigin: new Date(),
            catchDateTime: format(new Date(), 'yyyy-MM-dd HH:mm'),
            postDateTime: "",

            fish: "",
            weightOfFish: "",
            listOfCatches: [],
            fishpondName:""
          }
    },
    created() {
        axios.get('http://localhost:5000/api/Helyszinek/')
            .then(response => {response.data;
            this.lakes = response.data;})
          .catch(error => console.log(error))
    },
  methods: {
          getData(){
            axios.get('http://localhost:5000/api/Fogasok/'+this.identifierNumber.toString())
            .then(response => {response.data;
            this.listOfCatches = response.data;})
          .catch(error => console.log(error))},

            onCreatePost() {
              if (this.fishpondNumber == "") {
                this.fishpondNumber = null;
              }
              axios.post('http://localhost:5000/api/Fogasok',
              {azonosito: this.identifierNumber,
              helyszinID: this.fishpond,
              horgaszhely: this.fishpondNumber,
              datum_idopont: this.postDateTime,
              halfaj: this.fish,
              suly: this.weightOfFish})
              .then(this.showAlert(),)
            },

            // Bug vagy hiba esetén előfordulhat hogy az automatán beírt engedélyszám rossz
            // pl.: adatbázisban el van írva...vagy rendszerhiba, bug
            verifyIdentifierNumber(){
            if (this.regexIdentifierNumber.test(this.identifierNumber))
            {
              this.wrongIdentifierNumber = false
            } else {
              this.$swal.fire({
                  icon: 'error',
                  title: 'Hiba!!',
                  text: 'Frissítse a böngészőjét!',
                  confirmButtonText: 'Ok'})
              this.wrongIdentifierNumber = true
              }},

            verifyFishpondNumber(){
            if (this.regexFishpond.test(this.fishpondNumber) || this.fishpondNumber === ""
             || this.fishpondNumber === "Nincs" || this.fishpondNumber === "nincs") {
              this.wrongFishpondNumber = false 
            } else {
              this.$swal.fire({
                  icon: 'error',
                  title: 'Hiba!!',
                  text: 'Ellenőrizze a megadott horgászhelyet!',
                  confirmButtonText: 'Ok'})
              this.wrongFishpondNumber = true
              }},

            // Inputtal volt értelme a dátum validálásnak, ezt már a program adja meg

            // verifyCatchDateTime(){
            // var today = new Date();
            // var day = null;
            // var hour = parseInt(this.catchDateTime[11]+this.catchDateTime[12]);
            // if (today.getDate().toString().length === 1 && today.getHours() === hour) 
            // { day = parseInt(this.catchDateTime[9])
            //     if (day === today.getDate()) {
            //     this.wrongCatchDateTime=false 
            // } else {
            //     this.$swal.fire({
            //       icon: 'error',
            //       title: 'Hiba!!',
            //       text: 'Frissítse a böngészőjét!',
            //       confirmButtonText: 'Ok'})
            //   this.wrongCatchDateTime=true
            //   }}

            // else {
            //   day = parseInt(this.catchDateTime[8]+this.catchDateTime[9])
            //   if (day === today.getDate() && today.getHours() === hour) {
            //     this.wrongCatchDateTime=false
            // } else {
            //   this.$swal.fire({
            //       icon: 'error',
            //       title: 'Hiba!!',
            //       text: 'Frissítse a böngészőjét!',
            //       confirmButtonText: 'Ok'})
            //   this.wrongCatchDateTime=true
            //   }}
            // },

            catchReport(){
                if (this.identifierNumber != "" && this.fishpond != "" &&
                this.catchDateTime != "" && this.fish != "" && this.weightOfFish != "" && !this.wrongIdentifierNumber &&
                !this.wrongFishpondNumber) {
                    this.cacthSubmit=true
                } else {this.cacthSubmit=false}
            },

            send(){
            if (this.fishpondNumber == "") {
              this.fishpondNumber="Nincs"
            }
            this.postDateTime ='20'+(this.catchDateTimeOrigin.getYear().toString())[1]+
              (this.catchDateTimeOrigin.getYear().toString())[2]+'-'+
              ((this.catchDateTimeOrigin.getMonth()+1) < 10 ? '0'+((this.catchDateTimeOrigin.getMonth()+1).toString()) : ((this.catchDateTimeOrigin.getMonth()+1).toString()))+'-'+
              ((this.catchDateTimeOrigin.getDate()) < 10 ? '0'+(this.catchDateTimeOrigin.getDate().toString()) : (this.catchDateTimeOrigin.getDate().toString()))+
              'T'+((this.catchDateTimeOrigin.getHours()) < 10 ? '0'+(this.catchDateTimeOrigin.getHours().toString()) : (this.catchDateTimeOrigin.getHours().toString()))+':'
              +((this.catchDateTimeOrigin.getMinutes()) < 10 ? '0'+(this.catchDateTimeOrigin.getMinutes().toString()) : (this.catchDateTimeOrigin.getMinutes().toString()))+':'
              +((this.catchDateTimeOrigin.getSeconds()) < 10 ? '0'+(this.catchDateTimeOrigin.getSeconds().toString()) : (this.catchDateTimeOrigin.getSeconds().toString()));

            axios.get('http://localhost:5000/api/Helyszinek/'+this.fishpond.toString())
              .then(response => {response.data;
              this.fishpondName = response.data.vizterulet_neve;})
            .catch(error => console.log(error))
            },

            back(){
              if (this.fishpondNumber == "Nincs") {
              this.fishpondNumber=""
            }
              this.$swal.fire({
                icon: 'info',
                title: 'Visszalépett',
                text: 'Kérem nézze át a megadott adatokat!',
                confirmButtonText: 'Ok'});
              this.show=true
            },
            
            backSend(){
              this.show=true
              this.fishpond ='',
              this.fishpondNumber= "",
              this.catchDateTime= format(new Date(), 'yyyy-MM-dd HH:mm'),
              this.fish= "",
              this.weightOfFish= "",
              this.cacthSubmit =! this.cacthSubmit
            },

      showAlert() {
          this.$swal.fire({
          icon: 'success',
          title: 'Sikeres jelentés!',
          text: 'A fogás regisztrációja sikeres volt!',
          confirmButtonText: 'Ok'});
    },
  },
}
</script>
<style scoped>

#main{
  margin: auto;
  max-width: 1920px;
  min-height: 1080px;
  background-size: cover;
  min-height: 100vh;
}

option {
  width: 370px;
  white-space:normal;
  word-break: break-all;
  text-align:center;
}

i{
 font-size: 2rem;
 margin-right: 10px;
}

#background {
	background-image: url('~@/assets/wave.jpg');
	background-size: cover;
  min-height: 100vh;
}

#fishingLicence{
  font-size: 2.5rem;
  margin-bottom: 30px;
}

.form {
  background-color: rgba(255, 255, 255, 0.924);
	min-height: 90vh;
  max-width: 500px;
  margin: auto;
	box-shadow: 10px 10px 15px  rgba(7, 7, 7, 0.5);
  flex: 1 1;
	padding: 8rem 1rem 1rem;
}

h1 {
	font-family: 'Zen Antique', serif;
	font-size: 2rem;
	text-transform: uppercase;
	margin-bottom: 2.5rem;
  white-space: normal;
  text-align: center;
}

label {
  font-family: 'Zen Antique', serif;
	font-size: 1.5rem;
	text-transform: uppercase;
  color: black;
  font-weight: bold;
}

.form-input {
  text-align:center;
	appearance: none;
	border: none;
	outline: none;
	background: none;
  cursor: pointer;
	display: block;
	width: 100%;
	max-width: 370px;
	margin: 0 auto;
	font-size: 1.5rem;
	margin-bottom: 2rem;
	padding: 0.5rem 0rem;
}

select {
  appearance: none;
	background: none;
  border: none;
  cursor: pointer;
	width: 100%;
	max-width: 370px;
	margin: 0 auto;
	font-size: 1.5rem;
	margin-bottom: 4rem;
	padding: 0.5rem 0rem;
  font-family: 'Zen Antique', serif;
  border-bottom: 2px solid #2c3e50;
  color: rgba(35, 49, 80, 0.836);
  font-size: bold;
  text-align: center;
}

select *{
  background: rgba(212, 212, 212, 0.527);
}

input::placeholder {
	font-family: 'Zen Antique', serif;
	color: rgba(35, 49, 80, 0.836);
  font-size: bold;
  text-align: center;
}

form input:not([type="submit"]) {
	color: #2c3e50;
	border-bottom: 2px solid #2c3e50;
}

#cacthSubmit {
  font-family: 'Zen Antique', serif ;
  font-weight: bold;
  font-size: 1.7rem;
  background-color: #fcfcfc00; 
	color: rgb(3, 182, 3);
	font-weight: 700;
	padding: 0.8rem 2rem;
	border-radius: 2rem;
	cursor: pointer;
	border: 1px solid rgba(255, 255, 255, 0);
  display: block;
  margin: auto;
} 

#confirmYES {
  font-family: 'Zen Antique', serif ;
  font-weight: bold;
  font-size: 1.1rem;
  font-weight: 700;
	padding: 0.8rem 2rem;
	border-radius: 2rem;
	cursor: pointer;
  display: block;
  margin: auto;
  border: none;
  margin-top: 30px;
  color: rgb(3, 182, 3);
  background: rgba(255, 255, 255, 0);
}

#confirmNO {
  font-family: 'Zen Antique', serif ;
  font-weight: bold;
  font-size: 1.1rem;
  font-weight: 700;
	padding: 0.8rem 2rem;
	border-radius: 2rem;
	cursor: pointer;
  display: block;
  margin: auto;
  border: none;
  margin-top: 30px;
  background: rgba(255, 255, 255, 0);
  color: red;
} 

#cacthSubmit:hover {
	background:rgb(3, 182, 3);
  color: white;
	box-shadow: -10px -10px 15px rgba(192, 192, 192, 0.12),
				10px 10px 15px  rgba(23, 151, 55, 0.5);
  border: 1px solid rgb(3, 182, 3);
}

#confirmYES:hover {
	background:rgb(3, 182, 3);
  color: white;
	box-shadow: -10px -10px 15px rgba(192, 192, 192, 0.12),
				10px 10px 15px  rgba(23, 151, 55, 0.5);
}

#confirmNO:hover {
	background: red;
  color: white;
	box-shadow: -10px -10px 15px rgba(192, 192, 192, 0.12),
				10px 10px 15px  rgba(173, 16, 16, 0.5);
}

#last {
  margin-bottom: 70px;
}

table {
  margin: auto;
}

h1 {
  padding-bottom: 15px;
  color: #05315cee;
}

.error {
  padding: 20px;
  max-width: 400px;
  background-color: #f44336;
  color: white;
  margin: 0 auto 15px auto;
}

#confirm-form {
  font-family: 'Zen Antique', serif ;
  background: rgb(255,255,255);
  color: black;
	font-weight: 700;
	padding: 0.8rem 2rem;
	text-transform: uppercase;
  max-width: 378px;
  margin: auto;
  padding-top: 50px;
} 

#date{
  font-size: 2rem;
}

</style>