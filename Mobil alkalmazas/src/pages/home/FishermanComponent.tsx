import { IonIcon, IonItem, IonLabel } from "@ionic/react";
import { mail, person } from "ionicons/icons";
import { Fisherman } from "../../data/horgaszok";
import './home.page';

interface IProps
{
    fisherman: Fisherman;
}

const FishermanComponent: React.FC<IProps> = (props: IProps): JSX.Element =>
{
    return (
    <IonItem routerLink={`/catches/${props.fisherman.azonosito}`}>
        <IonLabel className="ListItem">   
            <div className="Container">
                <div className="Icons">
                    <div>
                        <IonIcon icon={person}></IonIcon>
                    </div>
                    <div>
                        <IonIcon icon={mail}></IonIcon>
                    </div>
                </div>
                <div>
                    <p className="Bold">{props.fisherman.azonosito}</p>
                    <p className="Bold">{props.fisherman.email_cim}</p> 
                </div>
            </div> 
        </IonLabel>
    </IonItem>
    );
}
export default FishermanComponent;