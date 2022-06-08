import { IonPage, IonContent, IonCard, IonSearchbar, IonButton, IonList, IonListHeader, IonHeader, IonButtons, IonIcon, IonItem, IonLabel, IonToggle } from "@ionic/react";
import { moon } from "ionicons/icons";
import { useEffect, useState } from "react";
import { RouteComponentProps, withRouter } from "react-router";
import ErrorBoundary from "../../components/menu/errorBoundary";
import { NavButtons } from "../../components/menu/NavButtons";
import { Catch } from "../../data/horgaszok";
import { HTTP } from "../../service/http";
import { baseURL } from "../../service/URL";
import CatchesComponent from "../catches/CatchesComponent";
import './search.style.css';

interface Iprops extends RouteComponentProps<{id: string}>
{
}

const Search: React.FC<Iprops> = (props: Iprops): JSX.Element =>
{
  const [input, setInput] = useState<string>('');
  const [catches, setCatches] = useState<Catch[]>([]);

  useEffect(() => {
  },[input])

  const fetchData = async (): Promise<void> =>
  {
        const catchData: Catch[] | undefined = await HTTP(`${baseURL}Fogasok/${input}`);

        //console.log(catchData)

        if (catchData) {
          setCatches(catchData)
        }
        if (catchData?.length === 0) {
          alert("A megadott azonosító nem létezik!")
        }
    }

    const ifUndefined = (): string =>
    {
        if (input === "0") {
            return "Összes"
        }
        else { return input }
    }

    // A keresés gomb csak akkor aktív ha nem üres a keresés mező
    const DisabledButton = (): any =>
    {
        if(input !== "")
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
          <IonCard class="col-sm-12-col-md-4-col-6">
              <div className="block">
              <IonHeader>
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
                <div className="SearchBody">
                  <IonSearchbar showCancelButton="focus" value={input} onIonChange={(e: any)=> setInput(e.target.value)} className="SearchBar" type="number">
                  </IonSearchbar>
                    <div className="container">
                      <IonButton size="large" shape="round" expand="block" className="ButtonSearch" onClick={() =>fetchData()} disabled={DisabledButton()}>Keresés</IonButton>
                      <IonButton size="large" shape="round" routerLink="/home" className="ButtonHome" >Kezdőlap</IonButton>
                  </div>
                  <IonList className="List">
                          <IonListHeader>
                            <h3><b>{ifUndefined()} felhasználó fogásai</b></h3>
                          </IonListHeader>
                          <hr />

                          {
                              catches.map( (data: Catch, i: number) =>
                              <CatchesComponent catch={data} key={i}/>)
                          }      
                      </IonList>
                </div> 
            </div>
          </IonCard>
      </IonContent>
    </IonPage>
  );
}

export default withRouter(Search);
