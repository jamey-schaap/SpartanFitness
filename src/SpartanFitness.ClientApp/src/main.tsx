import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { AuthProvider } from "./contexts/AuthProvider";

console.log(
  "Note: Remember to disable React.StrictMode when using ShiftUp & ShiftDown.",
  "React.StrictMode will sometimes call functions twice.",
  "Since ShiftUp & ShiftDown are not pure functions the functionality will break.",
);

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <AuthProvider>
      <App />
    </AuthProvider>
  </React.StrictMode>,
);
