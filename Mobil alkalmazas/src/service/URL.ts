// local, böngészőből elérés
export const localhost = "http://localhost:5000/api/";

// production, sajátgép localhost címe amit az android studio elér: 127.0.0.1 helyett ez kell!!
const production = "http://10.0.2.2:5000/api/";

// production or stage, android buildnél a második elérési utat veszi figyelembe
export const baseURL: string = process.env.NODE_ENV === "production" ?  production : localhost;