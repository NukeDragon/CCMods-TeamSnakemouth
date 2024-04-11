using Nickel;

namespace NukeDragon.TeamSnakemouth;

/* Much like a namespace, these interfaces can be named whatever you'd like.
 * We recommend using descriptive names for what they're supposed to do.
 * In this case, we use the IDemoCard interface to call for a Card's 'Register' method */
internal interface ICard
{
    static abstract void Register(IModHelper helper);
}

internal interface IArtifact
{
    static abstract void Register(IModHelper helper);
}
