<template>
<main id="background">
  <div id="app" class="container">
    <h1>Fogások</h1>
    <table id="myTable" class="table-striped  table-bordered table-hover table-compact mt-5">
      <thead>
        <tr>
          <th>Engedély</th>
          <th>Időpont</th>
          <th>Vízterület neve</th>
          <th>Horgászhely</th>
          <th>Halfaj</th>
          <th>Súly</th>
        </tr>
      </thead>
    </table>
  </div>
</main>
</template>

<script>
import axios from "axios";
import $ from "jquery";

export default {
  created() {
      axios
        .get("http://localhost:5000/api/Fogasok/0")
        .then((response) => {
          $("#myTable").DataTable({
            language: {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Hungarian.json"
            },
            data: response.data,
            columns: [
              { data: "azonosito" },
              { data: "datum_idopont" },
              { data: "helyszin" },
              { data: "horgaszhely" },
              { data: "halfaj" },
              { data: "suly" },
            ],
          });
        })
        .catch((error) => console.log(error.response));
    },
}
</script>

<style scoped>
select{
  min-width: 50px;
}
option{
  font-size: 1rem;
  font-weight: bold;
}

.container {
  padding-top: 105px;
}

th{
  color: azure;
  background-color:rgba(0, 0, 0, 0.799);
}

h1 {
	font-family: 'Zen Antique', serif;
	font-size: 2.6rem;
	font-weight: bold;
	text-transform: uppercase;
	padding: 0rem 0rem 2rem 0rem;
  white-space:normal;
  word-break: break-all;
  text-align:center;
}

</style>