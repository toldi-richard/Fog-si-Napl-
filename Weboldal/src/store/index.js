import { createStore } from 'vuex'
import router from '../router'
import { auth } from '../firebase'
import { 
  signInWithEmailAndPassword,
  signOut,
  createUserWithEmailAndPassword 
} from 'firebase/auth'
import Swal from 'sweetalert2'

export default createStore({
  state: {
    user: null
  },
  mutations: {
    SET_USER (state, user) {
      state.user = user
    },

    CLEAR_USER (state) {
      state.user = null
    }

  },
  actions: {
    async login ({ commit }, details) {
      const { email, password } = details

      try {
        await signInWithEmailAndPassword(auth, email, password)
      } catch (error) {
        switch(error.code) {
          case 'auth/user-not-found':
            Swal.fire({
              title: 'Hiba!',
              text: 'A megadott email cím még nincs regisztrálva vagy helytelen!',
              icon: 'error',
              confirmButtonText: 'Ok',
              confirmButtonColor: '#2272d2'
            })
            break
          case 'auth/wrong-password':
            Swal.fire({
              title: 'Hiba!',
              text: 'Rossz jelszó!',
              icon: 'error',
              confirmButtonText: 'Ok',
              confirmButtonColor: '#2272d2'
            })
            break
          default:
            Swal.fire({
              title: 'Hiba!',
              text: 'A mezők kitöltése kötelező!',
              icon: 'error',
              confirmButtonText: 'Ok',
              confirmButtonColor: '#2272d2'
            })
        }
        return
      }

      commit('SET_USER', auth.currentUser)
      router.push('/')
    },
    async register ({ commit}, details) {
      const { email, password } = details

     try {
       await createUserWithEmailAndPassword(auth, email, password)
     } catch (error) {
       switch(error.code) {
         case 'auth/email-already-in-use':
          Swal.fire({
            title: 'Hiba!',
            text: 'A megadott email cím már regisztrálva van!',
            icon: 'error',
            confirmButtonText: 'Ok',
            confirmButtonColor: '#2272d2'
          })
           break
         case 'auth/invalid-email':
          Swal.fire({
            title: 'Hiba!',
            text: 'Helytelen email cím!',
            icon: 'error',
            confirmButtonText: 'Ok',
            confirmButtonColor: '#2272d2'
          })
           break
         case 'auth/operation-not-allowed':
          Swal.fire({
            title: 'Hiba!',
            text: 'A művelet nincs engedélyezve!',
            icon: 'error',
            confirmButtonText: 'Ok',
            confirmButtonColor: '#2272d2'
          })
           break
         case 'auth/weak-password':
          Swal.fire({
            title: 'Hiba!',
            text: 'Gyenge jelszó!',
            icon: 'error',
            confirmButtonText: 'Ok',
            confirmButtonColor: '#2272d2'
          })
           break
         default:
          Swal.fire({
            title: 'Hiba!',
            text: 'A mezők kitöltése kötelező!',
            icon: 'error',
            confirmButtonText: 'Ok',
            confirmButtonColor: '#2272d2'
          })
       }
       return
     }

     commit('SET_USER', auth.currentUser)

     router.push('/')
   },

    async logout ({ commit }) {
      await signOut(auth)
      commit('CLEAR_USER')
      router.push('/login')
    },

    fetchUser ({ commit }) {
      auth.onAuthStateChanged(async user => {
        if (user === null) {
          localStorage.clear();
          commit('CLEAR_USER')
        } else {
          commit('SET_USER', user)
          if (router.isReady() && router.currentRoute.value.path === '/login') {
            router.push('/')
          }
        }
      })
    }
  }
})
