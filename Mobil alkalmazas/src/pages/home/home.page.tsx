import React, { useCallback, useContext, useEffect, useState } from "react";
import {
  IonButtons,
  IonCard,
  IonCardTitle,
  IonContent,
  IonFooter,
  IonHeader,
  IonIcon,
  IonItem,
  IonLabel,
  IonList,
  IonListHeader,
  IonPage,
  IonToggle,
  NavContext
} 
from '@ionic/react';
import './home.style.css';
import { Fisherman } from '../../data/horgaszok';
import FishermanComponent from './FishermanComponent';
import { HTTP } from '../../service/http';
import { baseURL } from '../../service/URL';
import Loader from "react-loader-spinner";
import { NavButtons } from "../../components/menu/NavButtons";
import ErrorBoundary from "../../components/menu/errorBoundary";
import { addCircleOutline } from "ionicons/icons";
import { moon } from "ionicons/icons";

const Home: React.FC = (): JSX.Element =>
{
    const [fisherman, setHorgaszok] = useState<Fisherman[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const { navigate } = useContext(NavContext);
    const redirectToPostPage = useCallback(() => navigate("/post", "forward", "replace"), [navigate]);

    useEffect(()=> 
    {
        setTimeout(() => {
            setIsLoading(false);
        },1000);
        fetchData();
    },[]);

    // Loader komponens
    const loader = (): JSX.Element =>
    <div className="loader">
        <Loader
        type="Puff"
        color="black"
        height={100}
        width={100}
        visible={isLoading}
  /> </div> 

  // Fetch metódus
    const fetchData = async (): Promise<void> =>
    {
        const data: Fisherman[] | undefined = await HTTP(`${baseURL}Felhasznalok`);

        if (data) 
      {
        setHorgaszok(data);
      }
    }

    // Éjszakai-mód metódusa, ezt használva a .dark osztályú css formázások lépnek érvénybe
    const toggleDarkModeHandler = () => {
      document.body.classList.toggle("dark");
    }   

return (
  <IonPage className="Page">
      <IonContent fullscreen class="col-sm-12-col-md-4-col-6">
        <IonCard class="col-sm-12-col-md-4-col-6">
            <div className="block">
             <IonHeader className="Header">
              <IonButtons>
                <ErrorBoundary>
                    <NavButtons/>
                </ErrorBoundary>   
                </IonButtons>
             </IonHeader>
              <IonItem className="DarkModeToggle">
                <IonIcon slot="start" icon={moon} className="component-icon component-icon-dark"/>
                <IonLabel className="DarkModeToggle">Sötét Mód</IonLabel>
                <IonToggle
                  slot="end"
                  name="darkMode"
                  className="DarkModeToggle"
                  onIonChange={toggleDarkModeHandler}
                />
              </IonItem>
          <section className="container">
          <div className="shape">
            <div className="content">
              <IonCardTitle class="col-sm-12-col-md-4-col-6"><h1>Fogási Napló</h1></IonCardTitle>
              </div>
            </div>
            </section>
                <IonList className="List">
                  <IonListHeader>
                      <h3>Horgászok</h3>
                  </IonListHeader>
                  <hr />
                  {isLoading ?
                loader() :
                
                fisherman.map((data: Fisherman, i: number) => <FishermanComponent fisherman={data} key={i} />)}
                </IonList>
          </div>
        </IonCard>
      </IonContent>
      <IonFooter className="footer">
        <IonItem className="footer">
          <IonIcon className="footer"
                   slot="end" icon={addCircleOutline}
                   title="Add new catch"
                   color="primary"
                   onClick={() => redirectToPostPage()}/>
        </IonItem>
      </IonFooter>
      
    </IonPage>
 
  );

}

export default Home;