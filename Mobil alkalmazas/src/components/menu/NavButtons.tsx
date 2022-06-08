import { IonButton, IonMenuButton} from "@ionic/react";
import React from "react";
import { useEffect } from "react"
import './menu.style.css';

export const NavButtons = () => {

    const [mQuery, setMQuery] = React.useState<any>({
        matches: window.innerWidth > 768 ? true : false,
    });

    useEffect(() => {
        window.matchMedia("()min-width: 768px").addListener(setMQuery);
    },[]);
    
    return (
        <div>
            {mQuery && !mQuery.matches ? (
                <IonMenuButton/>
            ) : (
      <>
        <div className="MenuButtons">
            <IonButton size="large" routerLink={"/home"}><b>Horgászok | </b></IonButton>
            <IonButton size="large" routerLink={"/profil"}><b>Profil | </b></IonButton>
            <IonButton size="large" routerLink={"/search"}><b>Keresés |</b></IonButton>
            <IonButton size="large" routerLink={"/post"}><b>Fogás jelentése |</b></IonButton>
            <IonButton size="large" routerLink={"/"}><b>Kilépés</b></IonButton>
        </div>     
        </>    
    )}
      </div>
    );
    
}