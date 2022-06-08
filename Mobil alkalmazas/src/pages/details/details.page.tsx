import { IonPage, IonContent, IonCard, IonListHeader, IonButtons, IonBackButton, IonIcon, IonFooter, IonItem, IonLabel, IonToggle, IonHeader } from "@ionic/react";
import { useState, useEffect } from "react";
import Loader from "react-loader-spinner";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { Catch, Helyszin} from "../../data/horgaszok";
import { HTTP } from "../../service/http";
import { baseURL } from "../../service/URL";
import { NavButtons } from "../../components/menu/NavButtons";
import './details.style.css';
import { barbellOutline, calendarOutline, fishOutline, mapOutline, moon, navigateOutline, personOutline } from "ionicons/icons";
import ErrorBoundary from "../../components/menu/errorBoundary";

interface Iprops extends RouteComponentProps<{id: string}>
{
}
 const Details: React.FC<Iprops> = (props: Iprops): JSX.Element => {

    const [fogas, setFogas] = useState<Catch>();
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [helyszin, setHelyszin] = useState<Helyszin>();

    useEffect(() =>
    {
      fetchData();
    }, [props.match.params.id],);

    // Feth data method
    const fetchData = async (): Promise<void> =>
    {
        const id: string = props.match.params.id;
        const data: Catch | undefined = await HTTP(`${baseURL}Fogasok/${id}`);
        const fogasdata: Catch | undefined = await HTTP(`${baseURL}Fogasok/fogas/${id}`);
        
        if (data) {
          setFogas(fogasdata);
        }
        
        setTimeout(() =>
        {
          setIsLoading(false);
        }, 1500);

        const helyszindata: Helyszin | undefined = await HTTP(`${baseURL}Helyszinek/${fogasdata?.helyszinID}`);
        if (helyszindata) {
          setHelyszin(helyszindata)
    }
  }
  // Loader komponens
    const loader = (): JSX.Element =>
    <div className="loader">
        <Loader type="Puff"
                color="black"
                height={100}
                width={100}
                visible={isLoading}
        /> </div> 

        // Ha a horgászhely mező üres, akkor a "Nincs adat"-ot írtajuk ki
        const ifNull = (): string=> 
        {
          if (fogas?.horgaszhely === undefined || fogas.horgaszhely === null || fogas.horgaszhely === "") 
            return "Nincs adat"
          return fogas?.horgaszhely.toString()
        }

        // Éjszakai-mód metódusa
        const toggleDarkModeHandler = () => {
          document.body.classList.toggle("dark");
        }

    return(       
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
            <section className="container">
              <div className="shape">
                <div className="content">
                  <IonListHeader >      
                    <h1>Fogás részletei</h1>
                  </IonListHeader>
                  </div>
                </div>
              </section>
                {isLoading ?
                        loader() :
                      <div className="DetailsContainer" > 
                        <IonCard className="DetailsCard">
                          <IonIcon className="DetailIcon" icon={personOutline} size="large"/>
                          <br />
                          <span className="detailSpan"><b>Horgász azonosító: </b><br />{fogas?.azonosito}</span>
                        </IonCard>
                            <br />
                        <IonCard className="DetailsCard">
                          <IonIcon className="DetailIcon"  icon={mapOutline} size="large"/>
                          <br />
                          <span className="detailSpan"><b>Helyszín: </b><br />{helyszin?.vizterulet_neve}</span>
                        </IonCard>
                          <br />
                        <IonCard className="DetailsCard">
                          <IonIcon className="DetailIcon"  icon={navigateOutline} size="large"/>
                          <span className="detailSpan"><b>Horgászhely</b><br />{ifNull()}</span>                                                
                        </IonCard>
                            <br />
                        <IonCard className="DetailsCard">
                          <IonIcon className="DetailIcon"  icon={calendarOutline} size="large"/>
                              <br />
                          <span className="detailSpan"><b>Dátum: </b><br />{fogas?.datum_idopont}</span>                         
                        </IonCard>
                            <br />
                        <IonCard className="DetailsCard">
                          <IonIcon className="DetailIcon"  icon={fishOutline} size="large"/>
                          <br />
                          <span className="detailSpan"><b>Halfaj: </b><br />{fogas?.halfaj}</span>                        
                        </IonCard>
                            <br />
                        <IonCard className="DetailsCard">
                          <IonIcon className="DetailIcon"  icon={barbellOutline} size="large"/>
                            <br />
                          <span className="detailSpan"><b>Súly: </b><br />{fogas?.suly} kg</span>
                        </IonCard>
                      </div>}           
                </div>
              </IonCard>
          </IonContent>
            <IonFooter className="Footer">
              <IonBackButton text="Vissza" className="BackButton" defaultHref="Fogasok/${id}"></IonBackButton>
            </IonFooter>
        </IonPage>
 
    );
}

export default withRouter(Details);