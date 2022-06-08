import { createRouter, createWebHistory } from 'vue-router'
import Post from '../views/Fisherman/Post'
import Login from '../views/Entry/Login'
import Signup from '../views/Entry/Signup'
import Profile from '../views/Public/Profile'
import MyCatches from '../views/Fisherman/MyCatches'
import Catches from '../views/Public/Catches'
import Lakes from '../views/Public/Lakes'
import Statistic from '../views/Public/Statistic'
import GuardCatches from '../views/Fishguard/GuardCatches'
import Users from '../views/Administrator/Users'
import GuardUsers from '../views/Fishguard/CardUsers'
import { auth } from '../firebase'

const routes = [
  {
    path: '/',
    name: 'Profile',
    component: Profile,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/signup',
    name: 'Signup',
    component: Signup
  },
  {
    path: '/post',
    name: 'Post',
    component: Post,
    meta: {
      requiresAuth: true
    }
  },  
  {
    path: '/myCatches',
    name: 'MyCatches',
    component: MyCatches,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/catches',
    name: 'Catches',
    component: Catches,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/lakes',
    name: 'Lakes',
    component: Lakes,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/statistic',
    name: 'Statistic',
    component: Statistic,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/guardCatches',
    name: 'GuardCatches',
    component: GuardCatches,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/users',
    name: 'Users',
    component: Users,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/guardUsers',
    name: 'GuardUsers',
    component: GuardUsers,
    meta: {
      requiresAuth: true
    }
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach((to, from, next) => {
  if (to.path === '/login' && auth.currentUser) {
    next('/')
    return;
  }

  if (to.matched.some(record => record.meta.requiresAuth) && !auth.currentUser) {
    next('/login')
    return;
  }

  next();
})


export default router