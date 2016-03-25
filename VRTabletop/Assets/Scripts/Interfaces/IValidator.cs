using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRTabletop.Pawns.Validation {
    public interface IValidator<T> {
        void StartValidation();

        T CheckValid();

        void StopValidation();

        IValidator<T> RetrieveValidator();
    }
}
