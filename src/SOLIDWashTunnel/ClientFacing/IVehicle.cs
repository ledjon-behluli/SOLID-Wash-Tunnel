namespace SOLIDWashTunnel.ClientFacing
{
    /* 
    * Pattern: 
    *   Visitor
    *
    * Reason: 
    *   Separate an algorithm from an object structure on which it operates.
    *   We are sepparating the IWashAction from the IVehicle, because the wash action simple executes 
    *   and does not care about wether it reaches the vehicle or not (like a stream of watter for exmp).
    *   
    *   IWashAction - Is the 'visitor' since it acts on the IVehicle.
    *   IVehicle - Is the 'abstract visited element', since it accepts an action from the visitor.
    *   
    * Learn more: 
    *   https://en.wikipedia.org/wiki/Visitor_pattern
    */

    public interface IWashAction
    {
        int CleaningFactor { get; }
        void Act(IVehicle vehicle);
    }

    public interface IVehicle
    {
        int Dirtiness { get; }
        void Accept(IWashAction action);
    }
}
