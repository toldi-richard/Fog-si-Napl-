import { IonBackButton, IonButton, IonButtons, IonCard, IonCardTitle, IonContent, IonDatetime, IonFooter, IonHeader, IonIcon, IonInput, IonItem, IonLabel, IonPage, IonSelect, IonSelectOption, IonToggle, NavContext} from "@ionic/react";
import React, { useCallback, useContext, useEffect, useState } from "react";
import { NavButtons } from "../../components/menu/NavButtons";
import './post.catch.style.css';
import axios from 'axios';
import {Fisherman } from "../../data/horgaszok";
import { HTTP } from "../../service/http";
import { baseURL } from "../../service/URL";
import { withRouter } from "react-router-dom";
import { moon } from "ionicons/icons";

const PostCatch: React.FC= () : JSX.Element =>
{
    const [azonosito, setAzonosito] = useState<string>('');
    const [helyszin, setHelyszin] = useState<string>('');
    const [horgaszhely, setHorgaszhely] = useState<string>('');
    const [idopont, setIdopont] = useState<string>('');
    const [halfaj, setHalfaj] = useState<string>('');
    const [suly, setSuly] = useState<string>('');
    const [fisherman, setFisherman] = useState<Fisherman[]>([]);

  useEffect(() => {
  },[azonosito])

    const { navigate } = useContext(NavContext);
    const redirectToHomePage = useCallback(() => navigate("/home", "forward", "replace"), [navigate]);
    
    //Teszt
    // console.log(azonosito, helyszin, horgaszhely,idopont,halfaj,suly)

    const submit = async (): Promise<void> =>
    {
          const fisherman: Fisherman[] | undefined = await HTTP(`${baseURL}Felhasznalok/${azonosito}`);
  
          if (fisherman) {
            setFisherman(fisherman)
            if (fisherman?.length === 0) {
                alert("A megadott azonosító nem létezik!")  
              }
              else if (fisherman?.length !== 0) {

                // 'http://localhost:5000/api/Fogasok' böngésző esetén
                axios.post(baseURL+'Fogasok',{
                      azonosito: azonosito,
                      helyszinID: helyszin,
                      horgaszhely: horgaszhely,
                      datum_idopont: idopont,
                      halfaj: halfaj,
                      suly: suly
                  })
                  alert("Sikeres beküldés!")  
                  redirectToHomePage()
              }
          }   
      }
      // Ha a horgászhely üres, akkor a "Nincs"-et küldje el adatbázisba
      const ifUndefined = (): any =>
      {
          if (horgaszhely === "") {
              setHorgaszhely("Nincs")
          }
      }
  
    // A mentés gomb csak akkor aktív, ha minden kötelező mező ki van töltve
    const DisabledButton = (): any =>
    {
        if(azonosito !== "" && helyszin !== "" && idopont !== "" && halfaj !== "" && suly !== "" )
        {
            return false
        }
        else
        {
            return true
        }
    }

    // Éjszakai-mód metódusa
    const toggleDarkModeHandler = () => {
        document.body.classList.toggle("dark");
      }

    return (   
    <IonPage className="Page">
    <IonContent fullscreen class="col-sm-12-col-md-4-col-6">
        <IonCard class="col-sm-12-col-md-4-col-6" >
            <div className="block" >
            <IonHeader className="Header">
            <IonButtons>
                    <NavButtons/>
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
            <IonCardTitle class="col-sm-12-col-md-4-col-6">
                <h1>Fogás jelentése</h1> 
            </IonCardTitle>
            </div>
            </div>
            </section>

                <div className="Form">
                    <IonLabel className="Label">Horgászengedély</IonLabel>
                    <IonInput value={azonosito} onIonChange={(e: any)=> setAzonosito(e.target.value)} className="inputField" placeholder="pl.:129817"></IonInput>

                        <IonLabel className="Label">Helyszín</IonLabel>
                        <IonSelect value={helyszin} onIonChange={(e: any)=> setHelyszin(e.target.value)}className="inputField" placeholder="Kötelező kitölteni!">
                            <IonSelectOption value="">Kötelező kitölteni!</IonSelectOption>
                            <IonSelectOption value="1">Kórógyi csatorna</IonSelectOption>
                            <IonSelectOption value="2">Bedegkéri halastó</IonSelectOption>
                            <IonSelectOption value="3">Tisza folyó Csongrág megyei szakasz</IonSelectOption>
                            <IonSelectOption value="4">Maros folyó </IonSelectOption>
                            <IonSelectOption value="5">Hármas-Körös </IonSelectOption>
                            <IonSelectOption value="6">Csongrádi vízlépcső munkatere</IonSelectOption>
                            <IonSelectOption value="7">Gyálai Holt-Tisza</IonSelectOption>
                            <IonSelectOption value="8">Körtvélyesi Holt-Tisza</IonSelectOption>
                            <IonSelectOption value="9">Akolszögi Holt-Tisza</IonSelectOption>
                            <IonSelectOption value="10">Maty-éri víztározó</IonSelectOption>
                            <IonSelectOption value="11">Deszki tavak</IonSelectOption>
                            <IonSelectOption value="12">Vértó</IonSelectOption>
                            <IonSelectOption value="13">Antal-tó</IonSelectOption>
                        </IonSelect>

                        <IonLabel className="Label">Horgászhely</IonLabel>
                        <IonInput value={horgaszhely} onIonChange={(e: any)=> setHorgaszhely(e.target.value)} className="inputField" placeholder="pl.: 1 vagy A/2"></IonInput>

                        <IonLabel className="Label">Időpont</IonLabel>
                        <IonDatetime value={idopont} onIonChange={(e: any)=> setIdopont(e.target.value)} className="inputField" placeholder="éééé.hh.nn --:--"></IonDatetime>

                        <IonLabel className="Label">Halfaj</IonLabel>
                        <IonSelect value={halfaj} onIonChange={(e: any)=> setHalfaj(e.target.value)} className="inputField" placeholder="Kötelező kitölteni!">
                            <IonSelectOption value="">Kötelező kitölteni!</IonSelectOption>
                            <IonSelectOption value="ponty">ponty</IonSelectOption>
                            <IonSelectOption value="amur">amur</IonSelectOption>
                            <IonSelectOption value="márna">márna</IonSelectOption>
                            <IonSelectOption value="compó">compó</IonSelectOption>
                            <IonSelectOption value="harcsa">harcsa</IonSelectOption>
                            <IonSelectOption value="csuka">csuka</IonSelectOption>
                            <IonSelectOption value="fogas-süllő">fogas-süllő</IonSelectOption>
                            <IonSelectOption value="kő-süllő">kő-süllő</IonSelectOption>
                            <IonSelectOption value="balin">balin</IonSelectOption>
                            <IonSelectOption value="sebes pisztráng">sebes pisztráng</IonSelectOption>
                            <IonSelectOption value="kecsege">kecsege</IonSelectOption>
                            <IonSelectOption value="széles kárász">széles kárász</IonSelectOption>
                            <IonSelectOption value="angolna">angolna</IonSelectOption>
                            <IonSelectOption value="menyhal">menyhal</IonSelectOption>
                            <IonSelectOption value="domolykó">domolykó</IonSelectOption>
                            <IonSelectOption value="garda">garda</IonSelectOption>
                            <IonSelectOption value="egyéb keszegfélék">egyéb keszegfélék</IonSelectOption>
                            <IonSelectOption value="ezüstkárász">ezüstkárász</IonSelectOption>
                            <IonSelectOption value="busák">busák</IonSelectOption>
                            <IonSelectOption value="törpeharcsák">törpeharcsák</IonSelectOption>
                            <IonSelectOption value="egyéb halfajta">egyéb halfajta</IonSelectOption>
                        </IonSelect>

                        <IonLabel className="Label">Súly</IonLabel>
                        <IonSelect value={suly} onIonChange={(e: any)=> {setSuly(e.target.value);ifUndefined()}} className="inputField" placeholder="Kötelező kitölteni!">
                            <IonSelectOption value="" className="inputField">Kötelező kitölteni!</IonSelectOption>
                            <IonSelectOption value="0.5" className="inputField">0.5</IonSelectOption>
                            <IonSelectOption value="1" className="inputField">1</IonSelectOption>
                            <IonSelectOption value="1.5" className="inputField">1.5</IonSelectOption>
                            <IonSelectOption value="2" className="inputField">2</IonSelectOption>
                            <IonSelectOption value="2.5" className="inputField">2.5</IonSelectOption>
                            <IonSelectOption value="3" className="inputField">3</IonSelectOption>
                            <IonSelectOption value="3.5" className="inputField">3.5</IonSelectOption>
                            <IonSelectOption value="4" className="inputField">4</IonSelectOption>
                            <IonSelectOption value="4.5" className="inputField">4.5</IonSelectOption>
                            <IonSelectOption value="5" className="inputField">5</IonSelectOption>
                            <IonSelectOption value="5.5" className="inputField">5.5</IonSelectOption>
                            <IonSelectOption value="6" className="inputField">6</IonSelectOption>
                            <IonSelectOption value="6.5" className="inputField">6.5</IonSelectOption>
                            <IonSelectOption value="7" className="inputField">7</IonSelectOption>
                            <IonSelectOption value="7.5" className="inputField">7.5</IonSelectOption>
                            <IonSelectOption value="8" className="inputField">8</IonSelectOption>
                            <IonSelectOption value="8.5" className="inputField">8.5</IonSelectOption>
                            <IonSelectOption value="9" className="inputField">9</IonSelectOption>
                            <IonSelectOption value="9.5" className="inputField">9.5</IonSelectOption>
                            <IonSelectOption value="10" className="inputField">10</IonSelectOption>
                            <IonSelectOption value="10.5" className="inputField">10.5</IonSelectOption>
                            <IonSelectOption value="11" className="inputField">11</IonSelectOption>
                            <IonSelectOption value="11.5" className="inputField">11.5</IonSelectOption>
                            <IonSelectOption value="12" className="inputField">12</IonSelectOption>
                            <IonSelectOption value="12.5" className="inputField">12.5</IonSelectOption>
                            <IonSelectOption value="13" className="inputField">13</IonSelectOption>
                            <IonSelectOption value="13.5" className="inputField">13.5</IonSelectOption>
                            <IonSelectOption value="14" className="inputField">14</IonSelectOption> 
                        </IonSelect>

                    <div className="ButtonSaveDiv">
                        <IonButton className="ButtonSave" size="large" shape="round" onClick={() =>submit()} disabled={DisabledButton()}>Mentés</IonButton>
                    </div>
                    </div>          
            </div>        
            </IonCard>
        </IonContent>
        <IonFooter className="Footer">
              <IonBackButton className="BackButton" text="Vissza" defaultHref="home"></IonBackButton>
            </IonFooter>
        </IonPage>
  );
}
export default withRouter(PostCatch);