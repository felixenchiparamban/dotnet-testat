﻿using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;
using System;
using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        // Alle Entitäten lesen
        [OperationContract]
        List<AutoDto> GetAutos();

        [OperationContract]
        List<KundeDto> GetKunden();

        [OperationContract]
        List<ReservationDto> GetReservationen();

        // Eine Entität anhand des Primärschlussels lesen
        [OperationContract]
        AutoDto GetAutoById(int id);

        [OperationContract]
        KundeDto GetKundeById(int id);

        [OperationContract]
        ReservationDto GetReservationById(int id);

        // Einfügen
        [OperationContract]
        bool InsertAuto(AutoDto auto);

        [OperationContract]
        bool InsertKunde(KundeDto kunde);

        [FaultContract(typeof(ReservationDto))]
        [FaultContract(typeof(FaultInvalidDateRange))]
        [FaultContract(typeof(FaultAutoUnavailable))]
        [OperationContract]
        bool InsertReservation(ReservationDto reservation);

        // Update
        [OperationContract]
        [FaultContract(typeof(AutoDto))]
        bool UpdateAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(KundeDto))]
        bool UpdateKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(ReservationDto))]
        [FaultContract(typeof(FaultInvalidDateRange))]
        [FaultContract(typeof(FaultAutoUnavailable))]
        bool UpdateReservation(ReservationDto reservation);

        // Löschen
        [OperationContract]
        bool DeleteAuto(AutoDto auto);

        [OperationContract]
        bool DeleteKunde(KundeDto kunde);

        [OperationContract]
        bool DeleteReservation(ReservationDto reservation);

        // Check availability of Auto
        [FaultContract(typeof(FaultAutoUnavailable))]
        [OperationContract]
        bool IsAutoAvailable(DateTime from, DateTime to, int kundenId, int reservationNr);
    }
}
