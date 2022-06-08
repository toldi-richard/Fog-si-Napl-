import { IonButton, IonCard, IonCardTitle, IonContent,IonIcon,IonInput, IonItem, IonLabel, IonPage, IonToggle, NavContext } from '@ionic/react';
import React, { useCallback, useContext, useState } from 'react';
import { HTTP } from '../../service/http';
import { baseURL } from '../../service/URL';
import { moon } from 'ionicons/icons';
import './Login.css';

const Login: React.FC = () => {

  const [id, setId] = useState<string | undefined | null>();
  const [password, setPassword] = useState<string | undefined | null>();


  const { navigate } = useContext(NavContext);
  const redirectToHomePage = useCallback(() => navigate("/home", "forward"), [navigate]);

  const toggleDarkModeHandler = () => {
    document.body.classList.toggle("dark");
  }


  // Teszt
  // console.log(id,password)

  // Az alkalmazás elküldi a login adatokat a webAPI-nak
  // ha az adatok léteznek az adatbázisban és helyesek
  // a webAPI OK response-t küld vissza és a felhasználó azonosítóját
  // hogy azt ellenőrizhesse

  // Nem végleges megoldás!
  
  // Ez csak egy ideiglenes megoldás, amíg a WebAPI nem képes
  // a JWT alkalmazására, annak hiányában jobb beléptető rendszert
  // a jelenlegi tudásunk szerint nem tudtunk kialakítani
  const ifAuth = async(): Promise<void>=> 
  { 
    const succesFull: number | undefined = await HTTP(`${baseURL}Felhasznalok/Halor/${id}/${password}`);

    if (id !=="" && password !=="") 
    {
      if(succesFull == id)
      {
        redirectToHomePage()
        if (id) {
          localStorage.setItem("id", id)
          
        }
        if (password) {
          localStorage.setItem("password", password)
        }
      }
      else
      {
        alert("A belépési adatok helytelenek!")
      }
    }
    else {
      alert("Az azonosító és jelszó megadása kötelező!")
    }
    setId("");
    setPassword("");
   
  }

  return (
    <IonPage className="Page">
      <IonContent fullscreen class="col-sm-12-col-md-4-col-6">
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
          <IonCard class="col-sm-12-col-md-4-col-6">
              <div className="loginBlock">

                <section className="containerLogin">
                  <div className="shapeLogin">
                    <div className="contentLogin">
                    <IonCardTitle class="col-sm-12-col-md-4-col-6"><h1 className='titleLogin'>Fogási Napló</h1></IonCardTitle>

                        <IonCard className="inputField">
                          <IonInput value={id} placeholder="Azonosító" onIonChange={ (e) => setId(e.detail.value)}></IonInput>
                        </IonCard>

                        <IonCard className="inputField">
                          <IonInput type="password" value={password} placeholder="Jelszó" onIonChange={ (e) => setPassword(e.detail.value)}></IonInput>
                        </IonCard>

                        <IonButton size="large" shape='round' className="LoginButton" onClick={() =>ifAuth()}>Belépés</IonButton>

                        <IonLabel title='Elfelejtette jelszavát?'><p className="forgottenPass">Jelszó emlékeztető</p></IonLabel>
                  </div>
                </div>
              </section>
            </div>
        </IonCard>
      </IonContent>
    </IonPage>
  );
};

export default Login;
