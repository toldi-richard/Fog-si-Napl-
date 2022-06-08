import { Redirect, Route } from 'react-router-dom';
import { IonApp, IonRouterOutlet } from '@ionic/react';
import { IonReactRouter } from '@ionic/react-router';

/* Core CSS required for Ionic components to work properly */
import '@ionic/react/css/core.css';

/* Basic CSS for apps built with Ionic */
import '@ionic/react/css/normalize.css';
import '@ionic/react/css/structure.css';
import '@ionic/react/css/typography.css';

/* Optional CSS utils that can be commented out */
import '@ionic/react/css/padding.css';
import '@ionic/react/css/float-elements.css';
import '@ionic/react/css/text-alignment.css';
import '@ionic/react/css/text-transformation.css';
import '@ionic/react/css/flex-utils.css';
import '@ionic/react/css/display.css';


/* Theme variables */
import './theme/variables.css';

import Home from './pages/home/home.page';
import CatchesPage from './pages/catches/catches.page'
import React from 'react';
import DetailsPage from './pages/details/details.page';
import { Menu } from './components/menu/menu';
import SearchPage from './pages/search/SearchPage';
import Login from './pages/login/Login';
import PostCatch from './pages/post.catch/post.catch';
import Profile from './pages/profile/profile.page';

// Jövőbeli fejlesztési, tervben van az útvonalak védetté tétele
// ha már a WebAPI képes a JWT kezelésére tervben van az 
// útvonalak védetté tétele, a bejelentkezési státusz függvényében!


const App: React.FC = () => (
  <IonApp>
    <IonReactRouter>

      <Menu/>
      
      <IonRouterOutlet id='main'>

        <Route exact path="/home">
          <Home />
        </Route>

        <Route exact path="/catches/:id">
          <CatchesPage />
        </Route>

        <Route exact path="/details/:id">
          <DetailsPage />
        </Route>

        <Route exact path="/post">
          <PostCatch />
        </Route>

        <Route exact path="/search">
          <SearchPage />
        </Route>

        <Route exact path="/profil">
          <Profile />
        </Route>

        <Route exact path="/">
          <Login />
        </Route>

        

      </IonRouterOutlet>
    </IonReactRouter>
  </IonApp>
);

export default App;
