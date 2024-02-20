import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import { getAuth } from "firebase/auth";


const firebaseConfig = {
    apiKey: "AIzaSyC_n3-ydKZCEH1NS37F5PGHbZY-hHBN4EU",
    authDomain: "budgetapp-c742f.firebaseapp.com",
    databaseURL: "https://budgetapp-c742f-default-rtdb.europe-west1.firebasedatabase.app",
    projectId: "budgetapp-c742f",
    storageBucket: "budgetapp-c742f.appspot.com",
    messagingSenderId: "805051676328",
    appId: "1:805051676328:web:13388af249f32f40c9349d",
    measurementId: "G-7EQKZNFEW0"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const auth = getAuth(app);
const analytics = getAnalytics(app);

export default app;
export { auth };