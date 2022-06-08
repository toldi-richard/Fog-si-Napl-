import { IonAvatar, IonItem, IonLabel, NavContext } from "@ionic/react";
import { Catch } from "../../data/horgaszok";
import './catches.style.css';

interface IProps
{
    catch: Catch;
}

const CatchesComponent: React.FC<IProps> = (props: IProps): JSX.Element =>
{
    // Minden halfajhoz különböző képet jelenítünk meg
    const imageLing = (fishType: string): string =>
    {
        switch(fishType)
        {
            case "ponty":
                return "./assets/images/ponty.png";
                break;
            case "amur":
                return "./assets/images/amur.jpg";
                break;
            case "márna":
                return "./assets/images/márna.jpg";
                break;
            case "compó":
                return "./assets/images/compo.jpg";
                break;
            case "harcsa":
                return "./assets/images/harcsa.jpg";
                break;
            case "csuka":
                return "./assets/images/csuka.png";
                break;
            case "fogas-süllő":
                return "./assets/images/fogas-süllő.jpg";
                break;
            case "kő-süllő":
                return "./assets/images/kő-süllő.jpg";
                break;
            case "balin":
                return "./assets/images/balin.jpg";
                break;
            case "sebes pisztráng":
                return "./assets/images/sebes pisztráng.png";
                break;
            case "kecsege":
                return "./assets/images/kecsege.jpg";
                break;
            case "széles kárász":
                return "./assets/images/széles kárász.jpg";
                break;
                case "angolna":
                    return "./assets/images/angolna.jpg";
                    break;
                case "menyhal":
                    return "./assets/images/menyhal.jpg";
                    break;
                case "sügér":
                    return "./assets/images/sügér.png";
                    break;
                case "domolykó":
                    return "./assets/images/domolykó.jpg";
                    break;
                case "garda":
                    return "./assets/images/garda.jpg";
                    break;
                case "egyéb keszegfélék":
                    return "./assets/images/egyéb keszegfélék.jpg";
                    break;           
                case "ezüstkárász":
                    return "./assets/images/ezüstkárász.jpg";
                    break;
                case "busák":
                    return "./assets/images/busák.jpg";
                    break;
                case "törpeharcsák":
                    return "./assets/images/törpeharcsák.jpg";
                    break;   
                case "egyéb":
                    return "./assets/images/egyéb.jpg";
                    break;  
                default:
                    return "";        
        }
    }

    return (
        <IonItem routerLink={`/details/${props.catch.fogasID}`}>
            <IonAvatar slot="start" className="Avatar">
                    <img src={imageLing(props.catch.halfaj)}/>
            </IonAvatar>
            <IonLabel className="ListItem">
                <p>{props.catch.datum_idopont}</p>
                <p>Halfaj: {props.catch.halfaj}</p>
            </IonLabel>
        </IonItem>
    );
}

export default CatchesComponent;