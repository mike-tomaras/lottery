using lottery.domain.Interfaces;

namespace lottery.application;

//TEST NOTE: we implement this random gen from yhe domain in the application 
//layer but if we used something more sofisticated as an 
//integration we would push this out to the outer layer of the onion
internal class NativeRandomGen : IRandomGenerator
{
    public int GetRandomInt(int min, int max)
    {
        return new Random().Next(min, max);
    }
}
