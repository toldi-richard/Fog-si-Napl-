import { IonButton, IonButtons, IonCard, IonCardTitle, IonContent,IonHeader,IonIcon,IonInput, IonItem, IonLabel, IonList, IonPage, IonToggle } from '@ionic/react';
import axios from 'axios';
import {  mail, personOutline, moon, settingsOutline, createOutline, lockOpenOutline } from 'ionicons/icons';
import React, { useEffect, useState } from 'react';
import Loader from 'react-loader-spinner';
import { RouteComponentProps, withRouter } from 'react-router';
import ErrorBoundary from '../../components/menu/errorBoundary';
import { NavButtons } from '../../components/menu/NavButtons';
import { Fisherman } from '../../data/horgaszok';
import { HTTP } from '../../service/http';
import { baseURL } from '../../service/URL';
import './profile.style.css';

interface Iprops extends RouteComponentProps<{id: string}>
{
}

const Profile: React.FC<Iprops> = (props: Iprops): JSX.Element => {

    const [fisherman, setFisherman] = useState<Fisherman>();
    const [email, setEmail] = useState<string>('');
    const [jelszo, setJelszo] = useState<string>('');
    const [isLoading, setIsLoading] = useState<boolean>(true);

  useEffect(()=> 
  {
      setTimeout(() => {
          setIsLoading(false);
      },1000);
      fetchData();
  },[props.match.params.id],);

  const loader = (): JSX.Element =>
  <div className="loader">
      <Loader
      type="Puff"
      color="black"
      height={100}
      width={100}
      visible={isLoading}
/> </div> 

  const fetchData = async(): Promise<void>=> 
  { 
    const data: Fisherman | undefined = await HTTP(`${baseURL}Felhasznalok/Halorok/`+localStorage.getItem("id"));

        if (data) 
      {
        setFisherman(data);
        console.log("Adatok" + data)
    }
      }


    const toggleDarkModeHandler = () => {
      document.body.classList.toggle("dark");
    }

    const isAdmin = (): string | undefined=>  {
      if(fisherman?.szerepkorID == 2)
      {
        return "Halőr"   
      }
      else if (fisherman?.szerepkorID == 1)
      {
        return "Admin"   
      }
      else if (fisherman?.szerepkorID == 3)
      {
        return "Felhasználó"   
      }
  }

  // kis ikonra kattintva az input aktív lesz , aztán a módosít gombra kattintva pedig elküldi a post-ot

  // jelszóhoz egy unhide-gomb: alapvetően hidden a jelszó
  const submit = async (): Promise<void> =>
    {
          const fisherman: Fisherman | undefined = await HTTP(`${baseURL}Felhasznalok/Halorok/`+localStorage.getItem("id"));
  
          if (fisherman) {
            setFisherman(fisherman)
            if (email === "" || jelszo === "") {
                alert("A jelszó és e-mail cím mezőt ki kell tölteni!") 
            }
              else if (email !== "" && jelszo !== "") {

                // 'http://localhost:5000/api/Felhasznalok' böngésző esetén
                // "http://10.0.2.2:5000/api/" AS esetén
                axios.put(baseURL+'Felhasznalok/'+localStorage.getItem("id"),{
                      azonosito: fisherman?.azonosito,
                      szerepkorID: fisherman?.szerepkorID,
                      jelszo: jelszo,
                      email_cim: email,
                      szerepkor: null                     
                  })
                  alert("Sikeres beküldés!")  
                  // console.log(fisherman?.azonosito)
                  // console.log(fisherman?.szerepkorID)
                  // console.log(jelszo)
                  // console.log(email)                 
              }
          }   
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
                  <IonCardTitle class="col-sm-12-col-md-4-col-6"><h1>Profil</h1></IonCardTitle>
                  </div>
                </div>
              </section>
              {
                  isLoading ? loader() :

                <IonList className="Profile">

                  <div className='ListItem'>
                  <IonIcon className="ProfileIcon" icon={personOutline}/>
                  <IonInput className='ProfileData' disabled={true}>{fisherman?.azonosito}</IonInput></div>

                  <hr className="line" />

                  <div className='ListItem'>
                  <IonIcon className="ProfileIcon"icon={settingsOutline}></IonIcon>
                  <IonInput className='ProfileData' disabled={true}>{isAdmin()}</IonInput></div>

                  <hr className="line" />

                  <div className='ListItem'>
                  <IonIcon className="ProfileIcon" icon={createOutline}slot='end'></IonIcon>
                  <IonIcon className="ProfileIcon"icon={lockOpenOutline}></IonIcon>
                  <IonInput value={jelszo} className='ProfileData' placeholder={fisherman?.jelszo} onIonChange={(e: any)=> setJelszo(e.target.value)}></IonInput></div>
                
                   
                  <hr  className="line"/>
                
                  <div className='ListItem'>
                  <IonIcon className="ProfileIcon" icon={createOutline}></IonIcon>
                  <IonIcon className="ProfileIcon"icon={mail}></IonIcon>
                  <IonInput value={email} className='ProfileData' onIonChange={(e: any)=> setEmail(e.target.value)} placeholder={fisherman?.email_cim}></IonInput>
                  
                  
                  </div>

                  

                 
                   
                  <hr  className="line"/>

                  


                  <IonButton className="LoginButton" disabled={false} onClick={() => submit()}>Módosítás</IonButton>
                </IonList> 
              }
                  </div>

        </IonCard>
      </IonContent>
    </IonPage>
  );
};

  export default withRouter(Profile);