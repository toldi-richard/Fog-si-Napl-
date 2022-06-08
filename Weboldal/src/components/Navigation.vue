<template>
   <header :class="{'scrolled-nav' : scrolledNav }">
       <nav  v-if="$store.state.user">
           <ul v-show="!mobile" class="navigation">
               <li><router-link  id="link3" class="link button" router-link to="/"><span class="fa fa-user"></span> Profil</router-link></li>
               <li v-if="user()"><router-link  id="link3" class="link button" router-link to="/MyCatches"><span class="fa fa-fish"></span> Fogásaim</router-link></li>
               <li v-if="user()"><router-link  id="link3" class="link button" router-link to="/Catches"><span class="fa fa-fish"></span> Fogások</router-link></li>
               <li v-if="user()"><router-link  id="link3" class="link button" router-link to="/Lakes"><span class="fa fa-water"></span> Vízterek</router-link></li>
               <li v-if="user()"><router-link  id="link3" class="link button" router-link to="/Post"><span class="fa fa-edit"></span> Fogás regisztráció</router-link></li>
               <li v-if="guard()"><router-link  id="link3" class="link button" router-link to="/GuardUsers"><span class="fa fa-users"></span> Horgászok</router-link></li>
               <li v-if="guard() || admin()"><router-link id="link3" class="link button" router-link to="/Statistic"><span class="fa fa-chart-bar"></span> Statisztika</router-link></li>
               <li v-if="guard() || admin()"><router-link id="link3" class="link button" router-link to="/GuardCatches"><span class="fa fa-fish"></span> Fogások</router-link></li>
               <li v-if="admin()"><router-link id="link3" class="link button" router-link to="/Users"><span class="fa fa-users"></span> Felhasználók</router-link></li>
               <button id="button" @click="$store.dispatch('logout')"><p class="fa fa-door-open"></p> Kijelentkezés</button>
               
           </ul>
           <div class="icon">
               <i @click="toggleMobileNav" v-show="mobile" class="fa fa-bars" :class="{'icon-active' :mobileNav}"></i>
           </div>
           <transition name="mobile-nav" @click="click()">
               <ul v-show="mobileNav" class="dropdown-nav">
                <li class="link"><p class="fa fa-list"></p> Menü </li>
                <li class="Hover" ><router-link id="link" class="link" router-link to="/"><p class="fa fa-user"></p> Profil</router-link></li>
                <li class="Hover" v-if="user()"><router-link id="link" class="link" router-link to="/MyCatches"><p class="fa fa-fish"></p> Fogásaim</router-link></li>
                <li class="Hover" v-if="user()"><router-link id="link" class="link" router-link to="/Catches"><p class="fa fa-fish"></p> Fogások</router-link></li>
                <li class="Hover" v-if="user()"><router-link id="link" class="link" router-link to="/Lakes"><p class="fa fa-water"></p> Vízterek</router-link></li>
                <li class="Hover" v-if="user()"><router-link id="link2" class="link" router-link to="/Post"><p class="fa fa-edit"></p> Fogás regisztráció</router-link></li>
                <li class="Hover" v-if="guard()"><router-link id="link5" class="link" router-link to="/GuardUsers"><p class="fa fa-users"></p> Horgászok</router-link></li>
                <li class="Hover" v-if="guard() || admin()"><router-link id="link4" class="link" router-link to="/Statistic"><p class="fa fa-chart-bar"></p> Statisztika</router-link></li>
                <li class="Hover" v-if="guard() || admin()"><router-link id="link4" class="link" router-link to="/GuardCatches"><p class="fa fa-fish"></p> Fogások</router-link></li>
                <li class="Hover" v-if="admin()"><router-link id="link5" class="link" router-link to="/Users"><p class="fa fa-users"></p> Felhasználók</router-link></li>
                <li class="Hover" id="buttonResp" @click="$store.dispatch('logout')">
                <p class="fa fa-door-open"></p> Kijelentkezés </li>
           </ul>
           </transition>
       </nav>
   </header>
</template>

<script scoped>
export default {
  name: "navigation",
  data() {
      return {
        scrolledNav: null,
        mobile: null,
        mobileNav: null,
        windowWidth: null,
        role: localStorage.getItem("role"),
      };
  },
   created () {
          window.addEventListener('resize', this.checkScreen);
          this.checkScreen();
      },
  methods: {
      toggleMobileNav() {
          this.mobileNav = !this.mobileNav;
      },
      click(){
        this.mobileNav = !this.mobileNav;
      },
      checkScreen(){
          this.windowWidth = window.innerWidth;
          if (this.windowWidth <= 1100) {
              this.mobile = true;
              return;
          }
          this.mobile = false;
          this.mobileNav = false;
          return;
      },
      user(){
          if(this.role == 3) {
              return true
          } else {return false}
      },
      guard(){
          if(this.role == 2) {
              return true
          } else {return false}
      },
    admin(){
          if(this.role == 1) {
              return true
          } else {return false}
      },
  }
};
</script>


<style scoped>
header {
    background-color: #13335c;
    z-index: 99;
    width: 100%;
    position: fixed;
    transition: .5s ease all;
}

nav {
    display: flex;
    flex-direction: row;
    padding: 12px 0;
    transition: .5s ease all;
    width: 90%;
    margin: 0 auto;
}

ul,.link {
    font-weight: 500;
    list-style: none;
    text-decoration: none;
}
span{
    margin-right: 10px;
}

#link {
    padding:20px 100px 30px 0px;
}
#link2 {
    padding:20px 10px 30px 0px;
}
#link3 {
    padding: 20px 10px 20px 10px;
}
#link3:hover {
    background: linear-gradient(90deg, #9ebd13 0%, #008552 100%);
    padding: 46px;
}
#link4 {
    padding:20px 92px 30px 0px;
}
#link5 {
    padding:20px 60px 30px 0px;
}

li {
    text-transform: uppercase;
    padding: 16px 0px 16px 16px;
}

.Hover:hover{
  background: linear-gradient(90deg, #9ebd13 0%, #008552 100%);
  transition: .5s ease all;
  font-weight: bold;
  font-size: 1.3rem;
}

.link {
    color: white;
    font-weight: bold;
    font-size: 14px;
    transition: .5s ease all;
    padding-bottom: 4px;
    border-bottom: 1px solid transparent;
}

.icon {
    display: flex;
    align-items: center;
    position: absolute;
    top: 0;
    right: 24px;
    height: 100%;
    color: white;
}

i {
    cursor: pointer;
    font-size: 24px;
    transition: 0.8s ease all;
}

p {
    margin-right: 5px;
}

.icon-active {
    transform: rotate(180deg);
}

.navigation {
    display: flex;
    align-items: center;
    flex: 1;
    justify-content: flex-end;
}

.dropdown-nav {
    display: flex;
    flex-direction: column;
    position: fixed;
    width: 100%;
    max-width: 220px;
    height: 100%;
    background-color: #13335c;
    top: 0;
    left: 0;
}

.mobile-nav-enter-active,
.mobile-nav-leave-active {
    transition: 1s ease all;
}

.mobile-nav-enter-from,
.mobile-nav-leave-to {
    transform: translateX(-250px);
}

.mobile-nav-enter-to {
    transform: translateX(0);
}

li {
    text-align: justify;
}

#button {
    text-align: justify;
    border: none;
    cursor: pointer;
    padding-top: 10px;
    text-transform: uppercase;
    background-color: rgba(255, 255, 255, 0);
    font-size: 14px;
    transition: .5s ease all;
    font-weight: bold;
    margin-left:30px;
    color: rgb(0, 0, 0); 
}
#buttonResp {
    padding: 20px 10px 20px 15px;
    text-align: justify;
    border: none;
    cursor: pointer;
    text-transform: uppercase;
    font-size: 14px;
    transition: .5s ease all;
    font-weight: bold;
    color: white; 
}

@media only screen and (min-width: 1135px) {
header {
    background-color: #13335c;
}
.button {
    color:white;
}
#button {
    color:white;
}}
</style>