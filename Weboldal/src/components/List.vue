<template>
        <button id="buttonFish" type="button" data-toggle="modal" data-target="#modal"><i class="fa fa-fish"></i></button>

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
                  <tr v-for="value, index in listdata" :key="index">
                    <td><i class="fa fa-clipboard"></i></td>
                    <td>{{ value.datum_idopont }}</td>
                    <td>{{ value.helyszin }}</td>
                    <td>{{ value.halfaj }}</td>
                    <td>{{ value.suly }}</td>
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
</template>

<script>

export default {
    name: "List",
    props: {
        listdata: {
            type: Array,
            requred: true,
            validator: (value) => {
                return value.length <= 5;
            }
        }
    }
}
</script>

<style scoped>
.table-hover tbody tr:hover td, .table-hover tbody tr:hover th {
  background-color:  #13335c;
  font-weight: bold;
  color: rgb(255, 255, 255);
  font-size: 1.3rem;
}

i{
 font-size: 2rem;
}

table{
    font-size: 1.1rem;
    background-color: white;
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

#buttonFish{
  position: relative;
  bottom: 100px;
  margin: 0% 0% 0% 78%;
  background-color: #fcfcfc00;
	color: rgb(0, 0, 0);
	padding: 1.2rem;
	border-radius: 2rem;
	cursor: pointer;
	border: 1px solid rgba(255, 255, 255, 0);
}

@media only screen and (min-width: 1100px) {
  #buttonFish {
    bottom: 30px;
  }
}

#button:hover{
  color:azure;
  background: rgba(196, 196, 196, 0.671);
	box-shadow: -10px -10px 15px rgba(192, 192, 192, 0.12),
				10px 10px 15px  rgba(0, 0, 0, 0.5);
}

#buttonFish:hover{
	background:rgb(3, 182, 3);
  color: white;
	box-shadow: -10px -10px 15px rgba(192, 192, 192, 0.12),
				10px 10px 15px  rgba(23, 151, 55, 0.5);
  border: 1px solid rgb(3, 182, 3);
}

</style>