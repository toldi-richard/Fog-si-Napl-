import React, { useCallback, useContext, useEffect, useState } from "react";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { Fisherman, Catch } from "../../data/horgaszok";
import { IonPage, IonContent, IonList, IonCard, IonCardTitle, IonButtons, IonBackButton, IonFooter, IonIcon, IonItem, NavContext, IonLabel, IonToggle, IonHeader } from "@ionic/react";
import { HTTP } from "../../service/http";
import { baseURL } from "../../service/URL";
import Loader from "react-loader-spinner";
import CatchesComponent from "./CatchesComponent";
import "./catches.style.css"
import { NavButtons } from "../../components/menu/NavButtons";
import ErrorBoundary from "../../components/menu/errorBoundary";
import { addCircleOutline, moon } from "ionicons/icons";

interface Iprops extends RouteComponentProps<{id: string}>
{
}

const CatchesPage: React.FC<Iprops> = (props: Iprops): JSX.Element =>
{
    const [catches, setCatches] = useState<Catch[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const { navigate } = useContext(NavContext);
    const redirectToPostPage = useCallback(() => navigate("/post", "forward", "replace"), [navigate]);

    useEffect(() =>
    {
      fetchData();
    }, [props.match.params.id],);

    // Fetc függvény
    const fetchData = async (): Promise<void> =>
    {
        const id: string = props.match.params.id;
        const fisherData: Fisherman | undefined = await HTTP(`${baseURL}Felhasznalok/${id}`);
        const data: Catch[] | undefined = await HTTP(`${baseURL}Fogasok/${id}`);

        if (data) {
          setCatches(data);
        }
        setTimeout(() =>
        {
          setIsLoading(false);
        }, 1000);
    }

    // Loader komponens
    const loader = (): JSX.Element =>
    <div className="loader">
        <Loader type="Puff"
                color="black"
                height={100}
                width={100}
                visible={isLoading}
        />
    </div>

  // Dark mode toggler method
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
                      <IonCardTitle class="col-sm-12-col-md-4-col-6"><h1>Fogási Napló</h1></IonCardTitle>
                      </div>
                    </div>
                          </section>
                      <IonList className="List">           
                        <IonItem>                        
                              <h3><b>Fogások listája</b></h3>
                             
                                <IonIcon className="PostPageButton"
                                        slot="end" icon={addCircleOutline}
                                        title="Add new catch"
                                        color="primary"
                                        onClick={() => redirectToPostPage()}/>
                          </IonItem>     
                            <hr />
                            {
                              isLoading ?
                              loader() :
                                catches.map( (data: Catch, i: number) =>
                                <CatchesComponent catch={data} key={i}/>) 
                            }                                 
                        </IonList>
                  </div>
                </IonCard>
            </IonContent>

            <IonFooter className="Footer">
              <IonBackButton className="BackButton" text="Vissza" defaultHref="Fogasok/${id}"></IonBackButton>
            </IonFooter>
          </IonPage>
    );
}

export default withRouter(CatchesPage);