import Ship from '../component/Ship';
import CreateShip from '../component/CreateShip';
import useFetch from './useFetch';

const shipService = () => {
  const { GET, POST, PUT } = useFetch();

  const postShip = async (ship: CreateShip, createdBy: string): Promise<Ship> => {
    const newShip = await POST<CreateShip, Ship>(
      `Ship/CreateShip?createdBy=${encodeURIComponent(createdBy)}`,
      ship
    );
    if (newShip) return newShip;
    throw new Error('Impossible de créer le bateau');
  };

  const getShip = async (id: string): Promise<Ship> => {
    const ship = await GET<Ship>(`Ship/GetShipId?id=${id}`);
    if (ship) return ship;
    throw new Error('Impossible de récupérer le bateau');
  };

  const getShips = async (): Promise<Ship[]> => {
    const ships = await GET<Ship[]>('Ship/GetAllShips');
    if (ships) return ships;
    throw new Error('Impossible de récupérer les bateaux');
  };

 const putShip = async (ship: Ship): Promise<Ship> => {
  const body = {
    name: ship.name,
    captain: ship.captain,
    crewSize: ship.crewSize,
    goldCargo: ship.goldCargo,
  };

  const updated = await PUT<object, Ship>(`Ship/UpdateShip?id=${ship.id}`, body);
  if (updated) return updated;
  throw new Error('Impossible de modifier le bateau');
};
  const deleteShip = async (id: string) => {
    await POST<object, void>(`Ship/DeleteShip?id=${id}`, {});
  };
  const getPorts = async (): Promise<string[]> => {
    const ports = await GET<string[]>('Ship/ports');
    if (ports) return ports;
    throw new Error('Impossible de récupérer les ports');
  };

  const sailShip = async (id: string, destination: string): Promise<void> => {
    const result = await POST<object, { message: string }>(
      `Ship/${id}/sail/${encodeURIComponent(destination)}`,
      {}
    );
    if (!result) {
      throw new Error("Impossible d'envoyer le bateau (port plein ou en réparation ?)");
    }
  };


  return { postShip, getShip, getShips, putShip, deleteShip,getPorts, sailShip };
};

export default shipService;