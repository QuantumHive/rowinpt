import React from "react";

export default () => {
    return (
        <div className="alert alert-danger mx-auto d-flex flex-column align-items-center text-center" role="alert">
            <h1>Oeps foutje! <i className="fa fa-frown-o" /></h1>
            
            <p>Er is een onverwachte fout opgetreden en het verzoek kan niet worden voltooid. Neem contact op met de technische dienst indien de fout blijft optreden.</p>
        </div>
    );
};