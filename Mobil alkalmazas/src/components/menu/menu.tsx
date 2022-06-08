import { IonMenu, IonHeader, IonTitle, IonContent, IonList, IonMenuToggle, IonItem, IonLabel, IonRouterOutlet, IonIcon, IonItemOption, NavContext, IonAvatar } 
from "@ionic/react"
import { addCircleOutline, gridOutline, homeOutline, logOutOutline, personOutline, searchOutline } from "ionicons/icons";
import { useContext, useCallback, useState } from "react";
import { Fisherman } from "../../data/horgaszok";
import { HTTP } from "../../service/http";
import { baseURL } from "../../service/URL";
import './menu.style.css';


export const Menu = () => {

    const { navigate } = useContext(NavContext);
    const redirectToHomePage = useCallback(() => navigate("/home", "forward", "replace"), [navigate]);
    const redirectToSearchPage = useCallback(() => navigate("/search", "forward", "replace"), [navigate]);
    const redirectToLoginPage = useCallback(() => navigate("/", "forward", "replace"), [navigate]);
    const redirectToPostPage = useCallback(() => navigate("/post", "forward", "replace"), [navigate]);
    const redirectToProfilePage = useCallback(() => navigate("/profil", "forward", "replace"), [navigate]);
    const [fisherman, setHorgaszok] = useState<Fisherman>();

    const fetchData = async(): Promise<void>=> 
    { 
      const data: Fisherman | undefined = await HTTP(`${baseURL}Felhasznalok/Halorok/`+localStorage.getItem("id"));
  
          if (data) 
        {
          setHorgaszok(data);
          console.log("Adatok" + data)
      }
        }
    
    return(
        <><IonMenu side="end" contentId='main' type="overlay" className="Menu">
            <IonHeader>
                <IonItem>
                    <IonIcon className="Icon" icon={gridOutline} color="dark" size="large"/>
                    <IonTitle className="menuTitle">Menü</IonTitle>
                </IonItem>
            </IonHeader>
            <IonItem>
            <div className="PersonAvatar">
                    <div>
                      <img src="./assets/icon/kisspng-user-profile-computer-icons-transparency-clip-art-huawei-mobile-cloud-5d0d154313f116.7977613315611384990817.png"/>
                    </div>
                    
                    <div>
                    <span>{fisherman?.email_cim}</span>
                    </div>
            </div>
            </IonItem>
            <IonContent>
                <IonList>
                    <IonMenuToggle auto-hide="false" className="Menu">
                        <IonItem button routerLink={"/home"} routerDirection="none">
                        <IonItemOption onClick={() => redirectToHomePage()} color="none">
                            <IonIcon className="Icon" icon={homeOutline} color="dark" size="large"/>
                        </IonItemOption>
                            <IonLabel className="menuItem">Horgászok</IonLabel>
                        </IonItem>
                    </IonMenuToggle>
                    <IonMenuToggle auto-hide="false" className="Menu">
                        <IonItem button routerLink={"/profil"} routerDirection="none">
                        <IonItemOption onClick={() => redirectToProfilePage()} color="none">
                            <IonIcon className="Icon" icon={personOutline} color="dark" size="large"/>
                        </IonItemOption>
                            <IonLabel className="menuItem">Profil</IonLabel>
                        </IonItem>
                    </IonMenuToggle>
                    <IonMenuToggle auto-hide="false" className="Menu">
                        <IonItem button routerLink={"/search"} routerDirection="none">
                                <IonItemOption onClick={() => redirectToSearchPage()} color="none">
                                    <IonIcon className="Icon" icon={searchOutline} color="dark" size="large"/>
                                </IonItemOption>
                            <IonLabel className="menuItem">Keresés</IonLabel>
                        </IonItem>
                    </IonMenuToggle> 
                    <IonMenuToggle auto-hide="false" className="Menu">
                        <IonItem button routerLink={"/post"} routerDirection="none">
                                <IonItemOption onClick={() => redirectToPostPage()} color="none">
                                    <IonIcon className="Icon" icon={addCircleOutline} color="dark" size="large"/>
                                </IonItemOption>
                            <IonLabel className="menuItem">Jelentés</IonLabel>
                        </IonItem>
                    </IonMenuToggle>  
                    <IonMenuToggle auto-hide="false" className="Menu">
                        <IonItem button routerLink={"/"} routerDirection="none">
                                <IonItemOption onClick={() => redirectToLoginPage()} color="none">
                                    <IonIcon className="Icon" icon={logOutOutline} color="dark" size="large"/>
                                </IonItemOption>
                            <IonLabel className="menuItem">Kilépés</IonLabel>
                        </IonItem>
                    </IonMenuToggle>  
                </IonList>
            </IonContent>
            </IonMenu><IonRouterOutlet></IonRouterOutlet></>
    );
}