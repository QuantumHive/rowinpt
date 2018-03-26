import React from "react";

export default ({ onSubmit, children }) => {
    return (
        <button type="button" className="btn btn-primary btn-lg btn-block" onClick={onSubmit}>
            {children === undefined ? "Opslaan" : children}
        </button>
    );
}